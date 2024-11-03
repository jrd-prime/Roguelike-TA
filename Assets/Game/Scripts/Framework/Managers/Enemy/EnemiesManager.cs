using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Scripts.Dto;
using Game.Scripts.Enemy;
using Game.Scripts.Framework.Configuration.SO.Enemy;
using Game.Scripts.Framework.Constants;
using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.Helpers;
using Game.Scripts.Framework.Managers.Experience;
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
using Animator = UnityEngine.Animator;

namespace Game.Scripts.Framework.Managers.Enemy
{
    public class EnemiesManager : MonoBehaviour, IEnemiesManager
    {
        public ReactiveProperty<int> Kills { get; } = new();
        public ReactiveProperty<int> KillToWin { get; } = new();
        public ReactiveProperty<int> EnemiesCount { get; } = new(0);

        [SerializeField] private int enemyPoolSize = 30;
        private bool _isStarted;

        private ISettingsManager _settingsManager;
        private List<EnemySettings> _enemiesSettingsList;
        private SpawnPointsManager _spawnPointsManager;
        private EnemyManagerSettings _enemyManagerSettings;
        private CustomPool<EnemyHolder> _enemyPool;
        private IPlayerModel _target;
        private IObjectResolver _container;
        private IPlayerModel _followTargetModel;
        private IExperienceManager _experienceManager;

        private readonly Dictionary<string, EnemyHolder> _enemiesCache = new();

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _container = container;
            _settingsManager = container.Resolve<ISettingsManager>();
            _spawnPointsManager = container.Resolve<SpawnPointsManager>();
            _followTargetModel = container.Resolve<IPlayerModel>();
            _experienceManager = container.Resolve<IExperienceManager>();
        }

        private void Awake()
        {
            Assert.IsNotNull(_settingsManager, $"Config manager is null. Add to auto inject!");
            Assert.IsNotNull(_spawnPointsManager, $"Spawn points manager is null. Add to auto inject!");
            _enemiesSettingsList = _settingsManager.GetConfig<EnemiesMainSettings>().enemies;
            _enemyManagerSettings = _settingsManager.GetConfig<EnemyManagerSettings>();

            _enemyPool =
                new CustomPool<EnemyHolder>(_enemyManagerSettings.enemyHolderPrefab, enemyPoolSize, transform,
                    _container, true);
        }

        private async void SpawnRandomEnemyAsync()
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

            var settingsDto = new EnemySettingsDto(animator, _followTargetModel, enemySettings);

            enemy.Initialize(settingsDto);

            _enemiesCache.Add(settingsDto.ID, enemy);

            UpdateEnemiesCount();

            return enemy;
        }

        private void UpdateEnemiesCount() => EnemiesCount.Value = _enemiesCache.Count;


        private void RemoveEnemy(string enemyId)
        {
            if (!_enemiesCache.ContainsKey(enemyId)) return;

            var enemy = _enemiesCache[enemyId];

            enemy.gameObject.SetActive(false);
            _enemiesCache.Remove(enemyId);
            enemy.ResetEnemy();
            _enemyPool.Return(enemy);

            UpdateEnemiesCount();
        }

        private void DespawnAllEnemies()
        {
            var enemyIds = _enemiesCache.Keys.ToList();
            foreach (var enemyId in enemyIds) RemoveEnemy(enemyId);

            _enemiesCache.Clear();
        }

        public async void EnemyDiedAsync(string enemyID)
        {
            if (!_enemiesCache.TryGetValue(enemyID, out EnemyHolder enemy)) return;

            AddKill();
            CheckWin();
            _experienceManager.AddExperience(enemy.EnemySettingsDto.Experience);

            enemy.enemyHUD.Hide();
            // remove from possible targets when searching for the nearest enemy
            enemy.tag = TagsConst.DeadEnemyTag;

            var enemyPosition = enemy.transform.position;
            var disappearPosition = new Vector3(enemyPosition.x, enemyPosition.y - 2, enemyPosition.z);
            var disappearDuration = enemy.EnemySettingsDto.DisappearanceDuration;

            // wait for death animation end
            await UniTask.Delay(AnimConst.DeathAnimationLengthMs);
            enemy.transform
                .DOMove(disappearPosition, disappearDuration)
                .SetEase(Ease.InOutQuad);

            // wait for enemy disappear
            await UniTask.Delay((int)(enemy.EnemySettingsDto.DisappearanceDuration * 1000));
            enemy.tag = TagsConst.EnemyTag;

            RemoveEnemy(enemyID);
        }

        private void CheckWin()
        {
            if (Kills.CurrentValue != KillToWin.CurrentValue) return;
            var stateMachine = _container.Resolve<StateMachine>();
            stateMachine.ChangeStateTo(StateType.Win);
        }

        private void AddKill() => Kills.Value += 1;

        // TODO fix enemies spawn
        public async void StartSpawnEnemiesAsync(int killToWin, int minOnMap, int maxOnMap, int spawnDelay)
        {
            if (_isStarted) return;

            Kills.Value = 0;
            KillToWin.Value = killToWin;
            KillToWin.ForceNotify();

            _isStarted = true;

            // TODO fix
            while (_isStarted)
            {
                var enemyCount = _enemiesCache.Count;

                if (enemyCount < minOnMap)
                {
                    var enemiesToSpawn = minOnMap - enemyCount;

                    for (var i = 0; i < enemiesToSpawn; i++)
                    {
                        if (!_isStarted) break;
                        SpawnRandomEnemyAsync();
                        await UniTask.Delay(spawnDelay);
                    }
                }
                else if (enemyCount < maxOnMap)
                {
                    SpawnRandomEnemyAsync();
                    await UniTask.Delay(spawnDelay);
                }

                if (!_isStarted) break;
                await UniTask.Delay(spawnDelay);
            }

            // var il = 0;
            // while (_isStarted && il < 1)
            // {
            //     SpawnRandomEnemyAsync();
            //     il++;
            // }
        }

        public void StopSpawn()
        {
            Debug.LogWarning("Stop spawn enemies " + _isStarted);
            if (!_isStarted) return;

            _isStarted = false;
            DespawnAllEnemies();
        }
    }
}
