using System;
using Game.Scripts.Enemy;
using Game.Scripts.Player;
using Game.Scripts.UI;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Framework.GameStateMachine
{
    public class GameManager : MonoBehaviour
    {
        [ReadOnly] public bool isGameStarted;
        private IObjectResolver _resolver;
        private EnemiesManager _enemiesManager;
        private PlayerModel _playerModel;
        private UIManager _uiManager;

        private readonly CompositeDisposable _disposables = new();
        private StateMachine _stateMachine;
        private bool isGamePaused;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            Debug.LogWarning("game manager construct");
            _resolver = resolver;
            _enemiesManager = _resolver.Resolve<EnemiesManager>();
            _playerModel = _resolver.Resolve<PlayerModel>();
            _uiManager = _resolver.Resolve<UIManager>();
        }


        public void GameOver()
        {
            Debug.LogWarning("GAME OVER");
            _enemiesManager.StopTheGame();
            isGameStarted = false;
        }

        public void StopTheGame()
        {
            _enemiesManager.StopTheGame();
            isGameStarted = false;
        }

        public void StartThegame()
        {
            if (isGameStarted) return;

            isGameStarted = true;
            _playerModel.ResetPlayer();
            _enemiesManager.StartTheGame();
        }

        public void Pause()
        {
            isGamePaused = true;
            Time.timeScale = 0;
        }

        public void UnPause()
        {
            isGamePaused = false;
            Time.timeScale = 1;
        }
    }
}
