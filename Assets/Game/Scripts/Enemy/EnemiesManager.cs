using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Framework;
using Game.Scripts.Framework.Configuration;
using Game.Scripts.Framework.ScriptableObjects;
using Game.Scripts.Framework.ScriptableObjects.Enemy;
using Game.Scripts.Framework.Systems.Follow;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;
using Random = UnityEngine.Random;

namespace Game.Scripts.Enemy
{
    public class EnemiesManager : MonoBehaviour, IEnemiesManager
    {
        private float _spawnDelay = 3f;
        private float _spawnTimer = 0f;
        private int _minEnemiesOnMap = 5;

        private Dictionary<string, EnemyHolder> _enemies = new();
        private IConfigManager _configManager;
        private List<EnemySettings> _enemiesSettingsList;
        private SpawnPointsManager _spawnPointsManager;
        private EnemyManagerSettings _enemyManagerSettings;

        private CustomPool<EnemyHolder> _enemyPool;
        private PlayerModel _target;

        Dictionary<string, GameObject> _enemyPrefabs = new();
        private IObjectResolver _container;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            Debug.LogWarning($"Enemies manager construct");
            _container = container;
            _configManager = container.Resolve<IConfigManager>();
            _spawnPointsManager = container.Resolve<SpawnPointsManager>();
            _target = container.Resolve<PlayerModel>();
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

        private async void Start()
        {
            for (int i = 0; i < 55; i++)
            {
                SpawnEnemy();

                await UniTask.Delay(500);
            }
        }


        public async void SpawnEnemy()
        {
            // Debug.LogWarning("Spawn enemy");
            // get object from pool
            EnemyHolder enemyHolder = _enemyPool.Get();


            // generate id
            var enemyId = Guid.NewGuid().ToString();

            // apply settings
            var enemySettings = _enemiesSettingsList[Random.Range(0, _enemiesSettingsList.Count)];


            // Debug.LogWarning("Start inst");
            var handle = await Addressables.InstantiateAsync(enemySettings.enemyPrefab, parent: enemyHolder.transform);

            // Debug.LogWarning("End inst");

            _enemyPrefabs.Add(handle.GetHashCode().ToString(), handle);

            enemyHolder.FillEnemySettings(enemyId, enemySettings, _target);

            // add enemy to cache
            _enemies.Add(enemyId, enemyHolder);

            // get spawn point
            Vector3 spawnPoint = _spawnPointsManager.GetRandomSpawnPointPosition();
            enemyHolder.OnSpawn();
            // spawn enemy
            enemyHolder.transform.position = spawnPoint;
            enemyHolder.gameObject.SetActive(true);
        }

        public void RemoveEnemy(string enemyId)
        {
            var enemy = _enemies[enemyId];

            enemy.gameObject.SetActive(false);

            // remove enemy from cache
            _enemies.Remove(enemyId);

            // return to pool
            _enemyPool.Return(enemy);
        }

        private void DespawnAllEnemies()
        {
            Debug.LogWarning("Despawn all enemies");
            foreach (var activeEnemy in _enemies)
            {
                activeEnemy.Value.OnDespawn();
                _enemyPool.Return(activeEnemy.Value);
            }
        }

        public void EnemyDie(string enemyID)
        {
            if (!_enemies.ContainsKey(enemyID))
            {
                Debug.LogWarning($"Enemy not found! {enemyID}");
                return;
            }

            Debug.LogWarning($"Enemy FOUND! {enemyID}");
            var enemy = _enemies[enemyID];

            enemy.ClearEnemySettings();

            _enemyPool.Return(enemy);
        }
    }

    public interface IEnemiesManager
    {
    }
}
