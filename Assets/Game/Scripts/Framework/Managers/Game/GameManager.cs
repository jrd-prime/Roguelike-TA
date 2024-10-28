using System;
using Game.Scripts.Framework.Managers.Enemy;
using Game.Scripts.Player.Interfaces;
using Game.Scripts.UI;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Managers.Game
{
    public class GameManager : MonoBehaviour, IDisposable
    {
        [SerializeField] private int spawnDelay = 500;
        [SerializeField] private int minEnemiesOnMap = 5;
        [SerializeField] private int maxEnemiesOnMap = 10;
        [SerializeField] private int killsToWin = 20;

        public ReactiveProperty<int> KillCount => _enemiesManager.Kills;
        public ReactiveProperty<int> KillToWin => _enemiesManager.KillToWin;

        [ReadOnly] public ReactiveProperty<bool> isGameStarted { get; } = new();
        private IObjectResolver _resolver;
        private EnemiesManager _enemiesManager;
        private IPlayerModel _playerModel;
        private UIManager _uiManager;

        private bool _isGamePaused;
        private readonly CompositeDisposable _disposables = new();


        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            Debug.LogWarning("game manager construct");
            _resolver = resolver;
            _enemiesManager = _resolver.Resolve<EnemiesManager>();
            _playerModel = _resolver.Resolve<IPlayerModel>();
            _uiManager = _resolver.Resolve<UIManager>();
        }

        private void Awake()
        {
            Assert.IsNotNull(_playerModel, $"PlayerModel is null. {this}");
            Assert.IsNotNull(_enemiesManager, $"EnemiesManager is null. {this}");
            Assert.IsNotNull(_uiManager, $"UIManager is null. {this}");
        }

        public void GameOver()
        {
            Debug.LogWarning("GAME OVER");
            _enemiesManager.StopSpawn();
            isGameStarted.Value = false;
        }

        public void StopTheGame()
        {
            _enemiesManager.StopSpawn();
            isGameStarted.Value = false;
        }

        public void StartNewGame()
        {
            if (isGameStarted.CurrentValue) return;

            isGameStarted.Value = true;
            _playerModel.ResetPlayer();

            _enemiesManager.StartSpawnEnemiesAsync(killsToWin, minEnemiesOnMap, maxEnemiesOnMap, spawnDelay);
        }

        public void Pause()
        {
            _isGamePaused = true;
            Time.timeScale = 0;
        }

        public void UnPause()
        {
            _isGamePaused = false;
            Time.timeScale = 1;
        }

        public void Dispose()
        {
            _resolver?.Dispose();
            _enemiesManager?.Dispose();
            _disposables?.Dispose();
            isGameStarted?.Dispose();
        }
    }
}
