using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Enemy;
using Game.Scripts.Framework.CommonModel;
using Game.Scripts.Framework.Configuration.SO.Enemy;
using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.Helpers;
using Game.Scripts.Framework.Managers.Settings;
using Game.Scripts.Framework.Managers.SpawnPoints;
using Game.Scripts.Framework.Providers.Pools;
using Game.Scripts.Player.Interfaces;
using Game.Scripts.UI;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using VContainer;
using Random = UnityEngine.Random;

namespace Game.Scripts.Framework.Managers.Enemy
{
    public class EnemiesManager : MonoBehaviour, IEnemiesManager
    {
        public ReactiveProperty<int> Kills { get; } = new();
        public ReactiveProperty<int> KillToWin { get; } = new();
        private bool _isStarted;

        private ISettingsManager _settingsManager;
        private List<EnemySettings> _enemiesSettingsList;
        private SpawnPointsManager _spawnPointsManager;
        private EnemyManagerSettings _enemyManagerSettings;
        private CustomPool<EnemyHolder> _enemyPool;
        private IPlayerModel _target;
        private IObjectResolver _container;
        private IPlayerModel _followTargetModel;

        private readonly Dictionary<string, EnemyHolder> _enemiesCache = new();

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _container = container;
            _settingsManager = container.Resolve<ISettingsManager>();
            _spawnPointsManager = container.Resolve<SpawnPointsManager>();
            _followTargetModel = container.Resolve<IPlayerModel>();
        }

        private void Awake()
        {
            Debug.LogWarning($"Enemies manager awake");
            Assert.IsNotNull(_settingsManager, $"Config manager is null. Add to auto inject!");
            Assert.IsNotNull(_spawnPointsManager, $"Spawn points manager is null. Add to auto inject!");
            _enemiesSettingsList = _settingsManager.GetConfig<EnemiesMainSettings>().enemies;
            _enemyManagerSettings = _settingsManager.GetConfig<EnemyManagerSettings>();

            _enemyPool =
                new CustomPool<EnemyHolder>(_enemyManagerSettings.enemyHolderPrefab, 100, transform, _container, true);
        }

        private async void SpawnRandomEnemy()
        {
            EnemySettings enemySettings = _enemiesSettingsList.GetRandomSettings();

            var enemy = await GetNewEnemyAsync(enemySettings);
            var spawnPoint = _spawnPointsManager.GetRandomSpawnPoint();

            enemy.transform.position = spawnPoint;
            enemy.gameObject.SetActive(true);
        }

        private async UniTask<EnemyHolder> GetNewEnemyAsync(EnemySettings enemySettings)
        {
            EnemyHolder enemy = _enemyPool.Get();

            await Addressables.InstantiateAsync(enemySettings.enemySkinPrefab, parent: enemy.transform);

            // find animator after skin is added
            var animator = enemy.GetComponentInChildren<Animator>();

            var settingsDto = new EnemySettingsDto
            {
                ID = Guid.NewGuid().ToString(),
                Animator = animator,
                Target = _followTargetModel,
                Speed = enemySettings.speed,
                AttackDelayMs = enemySettings.attackDelay,
                Damage = enemySettings.damage,
                Health = enemySettings.health,
            };

            enemy.Initialize(settingsDto);

            _enemiesCache.Add(settingsDto.ID, enemy);

            return enemy;
        }

        private void RemoveEnemy(string enemyId)
        {
            var enemy = _enemiesCache[enemyId];

            enemy.gameObject.SetActive(false);
            _enemiesCache.Remove(enemyId);
            enemy.ResetEnemy();
            _enemyPool.Return(enemy);
        }

        private void DespawnAllEnemies()
        {
            foreach (var enemy in _enemiesCache)
            {
                RemoveEnemy(enemy.Value.EnemyID);
            }

            _enemiesCache.Clear();
        }

        public void EnemyDie(string enemyID)
        {
            if (!_enemiesCache.ContainsKey(enemyID)) return;

            RemoveEnemy(enemyID);
            AddKill();
            CheckWin();
        }

        private void CheckWin()
        {
            if (Kills.CurrentValue != KillToWin.CurrentValue) return;
            StateMachine stateMachine = _container.Resolve<StateMachine>();
            stateMachine.ChangeStateTo(StateType.Win);
        }

        private void AddKill() => Kills.Value += 1;


        public async void StartSpawnEnemiesAsync(int killToWin, int minOnMap, int maxOnMap, int spawnDelay)
        {
            if (_isStarted) return;

            Kills.Value = 0;
            KillToWin.Value = killToWin;
            KillToWin.ForceNotify();

            _isStarted = true;

            while (_isStarted)
            {
                var enemyCount = _enemiesCache.Count;

                if (enemyCount < minOnMap)
                {
                    var enemiesToSpawn = minOnMap - enemyCount;

                    for (var i = 0; i < enemiesToSpawn; i++)
                    {
                        if (!_isStarted) break;
                        SpawnRandomEnemy();
                        await UniTask.Delay(spawnDelay);
                    }
                }
                else if (enemyCount < maxOnMap)
                {
                    SpawnRandomEnemy();
                    await UniTask.Delay(spawnDelay);
                }

                if (!_isStarted) break;
                await UniTask.Delay(spawnDelay);
            }
        }


        public void StopSpawn()
        {
            if (!_isStarted) return;

            _isStarted = false;
            DespawnAllEnemies();
        }

        public void Dispose()
        {
            _container?.Dispose();
            Kills?.Dispose();
            KillToWin?.Dispose();
            _enemiesCache.Clear();
        }
    }

    public struct EnemySettingsDto
    {
        public string ID;
        public Animator Animator;
        public ITrackableModel Target;
        public float Speed;
        public float AttackDelayMs;
        public float Damage;
        public float Health;
    }

    public interface IEnemiesManager
    {
        public void StartSpawnEnemiesAsync(int killToWin, int minOnMap, int maxOnMap, int spawnDelay);
        public void StopSpawn();
    }
}
