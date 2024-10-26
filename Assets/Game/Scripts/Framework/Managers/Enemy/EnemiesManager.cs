using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Enemy;
using Game.Scripts.Framework.Configuration.SO.Enemy;
using Game.Scripts.Framework.GameStateMachine;
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
    public class EnemiesManager : MonoBehaviour
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

        private async void SpawnEnemy()
        {
            EnemyHolder enemyHolder = _enemyPool.Get();
            EnemySettings enemySettings = GetRandomEnemySettings();

            await Addressables.InstantiateAsync(enemySettings.enemyPrefab, parent: enemyHolder.transform);

            var enemyId = Guid.NewGuid().ToString();
            enemyHolder.FillEnemySettings(enemyId, enemySettings, _followTargetModel);

            _enemiesCache.Add(enemyId, enemyHolder);

            var spawnPoint = _spawnPointsManager.GetRandomSpawnPointPosition();

            enemyHolder.Spawn(enemyId, enemySettings, spawnPoint, _followTargetModel);
        }

        private EnemySettings GetRandomEnemySettings() =>
            _enemiesSettingsList[Random.Range(0, _enemiesSettingsList.Count)];

        private void RemoveEnemy(string enemyId)
        {
            var enemy = _enemiesCache[enemyId];

            enemy.gameObject.SetActive(false);
            _enemiesCache.Remove(enemyId);
            enemy.ClearEnemySettings();
            _enemyPool.Return(enemy);
        }

        private void DespawnAllEnemies()
        {
            foreach (var activeEnemy in _enemiesCache)
            {
                activeEnemy.Value.ClearEnemySettings();
                _enemyPool.Return(activeEnemy.Value);
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


        public async void StartSpawnEnemies(int killToWin, int minOnMap, int maxOnMap, int spawnDelay)
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
                        SpawnEnemy();
                        await UniTask.Delay(spawnDelay);
                    }
                }
                else if (enemyCount < maxOnMap)
                {
                    SpawnEnemy();
                    await UniTask.Delay(spawnDelay);
                }

                if (!_isStarted) break;
                await UniTask.Delay(spawnDelay);
            }
        }


        public void StopTheGame()
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
}
