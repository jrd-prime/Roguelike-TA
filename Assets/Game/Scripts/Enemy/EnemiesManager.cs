using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Framework;
using Game.Scripts.Framework.Configuration;
using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.ScriptableObjects.Enemy;
using Game.Scripts.Player;
using Game.Scripts.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
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

        private int _enemiesToWin = 3;

        private Dictionary<string, EnemyHolder> _enemies = new();
        private IConfigManager _configManager;
        private List<EnemySettings> _enemiesSettingsList;
        private SpawnPointsManager _spawnPointsManager;
        private EnemyManagerSettings _enemyManagerSettings;

        private CustomPool<EnemyHolder> _enemyPool;
        private PlayerModel _followTargetModel;

        // Dictionary<string, GameObject> _enemyPrefabs = new();
        private IObjectResolver _container;

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
            Assert.IsNotNull(_enemiesSettingsList, $"Enemies settings list is null.");

            _enemyManagerSettings = _configManager.GetConfig<EnemyManagerSettings>();
            Assert.IsNotNull(_enemyManagerSettings, $"Enemy holder prefab is null.");

            _enemyPool =
                new CustomPool<EnemyHolder>(_enemyManagerSettings.enemyHolderPrefab, 100, transform, _container, true);
        }

        public async void SpawnEnemy()
        {
            // Debug.LogWarning("Spawn enemy");
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
            _enemies.Add(enemyId, enemyHolder);

            // get spawn point
            Vector3 spawnPoint = _spawnPointsManager.GetRandomSpawnPointPosition();

            enemyHolder.Spawn(enemyId, enemySettings, spawnPoint, _followTargetModel);

            Debug.LogWarning($"Spawn. Enemies in dictionary: {_enemies.Count}");
        }

        private EnemySettings GetRandomEnemySettings() =>
            _enemiesSettingsList[Random.Range(0, _enemiesSettingsList.Count)];


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
                activeEnemy.Value.ClearEnemySettings();
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

            _enemies.Remove(enemyID);

            enemy.ClearEnemySettings();

            _enemyPool.Return(enemy);
            Debug.LogWarning($"Die. Enemies in dictionary: {_enemies.Count}");

            if (_enemies.Count == 0)
            {
                Debug.LogWarning("<color=red>ALL ENEMIES DIED</color>");
                StateMachine stateMachine = _container.Resolve<StateMachine>();
                stateMachine.ChangeStateTo(UIType.Win);
            }
        }

        public async void StartTheGame()
        {
            if (isStarted) return;

            Debug.LogWarning("<color=green>SPAWN STARTED</color>");
            isStarted = true;
            for (int i = 0; i < _enemiesToWin; i++)
            {
                if (!isStarted) break;
                SpawnEnemy();

                await UniTask.Delay(500);
            }
        }

        public void StopTheGame()
        {
            if (!isStarted) return;

            Debug.LogWarning("<color=red>SPAWN STOPPED</color>");
            isStarted = false;
            DespawnAllEnemies();
        }

        public bool isStarted;
    }

    public interface IEnemiesManager
    {
    }
}
