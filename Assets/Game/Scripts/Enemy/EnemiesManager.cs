using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Framework;
using Game.Scripts.Framework.Configuration;
using Game.Scripts.Framework.ScriptableObjects;
using UnityEngine;
using UnityEngine.Assertions;
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
        private EnemyPool _enemyPool;
        private SpawnPointsManager _spawnPointsManager;
        private EnemyManagerSettings _enemyManagerSettings;

        private CustomPool<EnemyHolder> _enemyHolderPool;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            Debug.LogWarning($"Enemies manager construct");
            _configManager = container.Resolve<IConfigManager>();
            _spawnPointsManager = container.Resolve<SpawnPointsManager>();
        }

        private void Awake()
        {
            Debug.LogWarning($"Enemies manager awake");
            Assert.IsNotNull(_configManager, $"Config manager is null. Add to auto inject!");
            Assert.IsNotNull(_spawnPointsManager, $"Spawn points manager is null. Add to auto inject!");
            _enemiesSettingsList = _configManager.GetConfig<EnemiesMainSettings>().enemies;
            _enemyManagerSettings = _configManager.GetConfig<EnemyManagerSettings>();

            _enemyHolderPool =
                new CustomPool<EnemyHolder>(_enemyManagerSettings.enemyHolderPrefab, 100, transform, true);
        }

        private async void Start()
        {
            for (int i = 0; i < 300; i++)
            {
                SpawnEnemy();
        
                await UniTask.Delay(300);
            }
        
            DespawnAllEnemies();
        }

   

        public void SpawnEnemy()
        {
            Debug.LogWarning("Spawn enemy");
            // get object from pool
            EnemyHolder enemyHolder = _enemyHolderPool.Get();

            // generate id
            var enemyId = Guid.NewGuid().ToString();

            // apply settings
            enemyHolder.FillEnemySettings(enemyId, _enemiesSettingsList[Random.Range(0, _enemiesSettingsList.Count)]);

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
            foreach (var activeEnemy in new List<EnemyHolder>(_enemyHolderPool.GetActiveCount()))
            {
                activeEnemy.OnDespawn();
                _enemyHolderPool.Return(activeEnemy);
            }
        }
    }

    public interface IEnemiesManager
    {
    }
}
