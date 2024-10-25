using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Framework;
using Game.Scripts.Framework.Configuration;
using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.ScriptableObjects.Enemy;
using Game.Scripts.Player;
using Game.Scripts.UI;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using VContainer;
using Random = UnityEngine.Random;

namespace Game.Scripts.Enemy
{
    public class EnemiesManager : MonoBehaviour, IEnemiesManager
    {
        public ReactiveProperty<int> Kills { get; } = new();
        public ReactiveProperty<int> KillToWin { get; } = new();

        private Dictionary<string, EnemyHolder> _enemiesCache = new();
        private IConfigManager _configManager;
        private List<EnemySettings> _enemiesSettingsList;
        private SpawnPointsManager _spawnPointsManager;
        private EnemyManagerSettings _enemyManagerSettings;

        private CustomPool<EnemyHolder> _enemyPool;
        private PlayerModel _target;

        Dictionary<string, GameObject> _enemyPrefabs = new();
        private IObjectResolver _container;

        public bool isStarted;
        private PlayerModel _followTargetModel;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            Debug.LogWarning($"Enemies manager construct");
            _container = container;
            _configManager = container.Resolve<IConfigManager>();
            _spawnPointsManager = container.Resolve<SpawnPointsManager>();
            _followTargetModel = container.Resolve<PlayerModel>();
        }

        private void Awake()
        {
            Debug.LogWarning($"Enemies manager awake");
            Assert.IsNotNull(_configManager, $"Config manager is null. Add to auto inject!");
            Assert.IsNotNull(_spawnPointsManager, $"Spawn points manager is null. Add to auto inject!");
            _enemiesSettingsList = _configManager.GetConfig<EnemiesMainSettings>().enemies;
            _enemyManagerSettings = _configManager.GetConfig<EnemyManagerSettings>();

            _enemyPool =
                new CustomPool<EnemyHolder>(_enemyManagerSettings.enemyHolderPrefab, 100, transform, _container, true);
        }

        private async void SpawnEnemy()
        {
            // get object from pool
            EnemyHolder enemyHolder = _enemyPool.Get();

            // get enemy settings
            EnemySettings enemySettings = GetRandomEnemySettings();

            // instantiate enemy prefab to enemy holder
            await Addressables.InstantiateAsync(enemySettings.enemyPrefab, parent: enemyHolder.transform);

            // generate id and fill settings
            var enemyId = Guid.NewGuid().ToString();
            enemyHolder.FillEnemySettings(enemyId, enemySettings, _followTargetModel);

            // add enemy to cache
            _enemiesCache.Add(enemyId, enemyHolder);

            // get spawn point
            Vector3 spawnPoint = _spawnPointsManager.GetRandomSpawnPointPosition();

            // spawn enemy
            enemyHolder.Spawn(enemyId, enemySettings, spawnPoint, _followTargetModel);

            Debug.LogWarning($"Enemies spawned: {_enemiesCache.Count}");
        }

        private EnemySettings GetRandomEnemySettings() =>
            _enemiesSettingsList[Random.Range(0, _enemiesSettingsList.Count)];

        public void RemoveEnemy(string enemyId)
        {
            var enemy = _enemiesCache[enemyId];

            enemy.gameObject.SetActive(false);

            // remove enemy from cache
            _enemiesCache.Remove(enemyId);

            // return to pool
            _enemyPool.Return(enemy);
        }

        private void DespawnAllEnemies()
        {
            Debug.LogWarning("Despawn all enemies");
            foreach (var activeEnemy in _enemiesCache)
            {
                activeEnemy.Value.OnDespawn();
                activeEnemy.Value.ClearEnemySettings();
                _enemyPool.Return(activeEnemy.Value);
            }

            _enemiesCache.Clear();
        }

        public void EnemyDie(string enemyID)
        {
            if (!_enemiesCache.ContainsKey(enemyID))
            {
                Debug.LogWarning($"Enemy not found! {enemyID}");
                return;
            }

            Kills.Value += 1;

            var enemy = _enemiesCache[enemyID];

            _enemiesCache.Remove(enemyID);
            enemy.ClearEnemySettings();

            _enemyPool.Return(enemy);

            if (Kills.CurrentValue != KillToWin.CurrentValue) return;

            Debug.LogWarning("<color=red>WIN COMPLETE</color>");
            StateMachine stateMachine = _container.Resolve<StateMachine>();
            stateMachine.ChangeStateTo(StateType.Win);
        }

        public async void StartSpawnEnemies(int killToWin, int minOnMap, int maxOnMap, int spawnDelay)
        {
            if (isStarted) return;

            Kills.Value = 0;
            KillToWin.Value = killToWin;
            KillToWin.ForceNotify();

            Debug.LogWarning($"<color=green>SPAWN STARTED {_enemiesCache.Count}</color>");
            isStarted = true;


            while (isStarted)
            {
                var enemyCount = _enemiesCache.Count;


                if (enemyCount < minOnMap)
                {
                    var enemiesToSpawn = minOnMap - enemyCount;

                    for (var i = 0; i < enemiesToSpawn; i++)
                    {
                        if (!isStarted) break;
                        SpawnEnemy();
                        await UniTask.Delay(spawnDelay);
                    }
                }
                else if (enemyCount < maxOnMap)
                {
                    SpawnEnemy();
                    await UniTask.Delay(spawnDelay);
                }

                if (!isStarted) break;
                await UniTask.Delay(spawnDelay);
            }
        }


        public void StopTheGame()
        {
            if (!isStarted) return;

            Debug.LogWarning("<color=red>SPAWN STOPPED</color>");
            isStarted = false;
            DespawnAllEnemies();
        }

        public void Dispose()
        {
            _target?.Dispose();
            _container?.Dispose();
            _followTargetModel?.Dispose();
            Kills?.Dispose();
            KillToWin?.Dispose();
            _enemiesCache.Clear();
        }
    }

    public interface IEnemiesManager : IDisposable
    {
    }
}
