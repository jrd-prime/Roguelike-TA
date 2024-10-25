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

        private readonly CompositeDisposable Disposables = new();
        private StateMachine _stateMachine;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            Debug.LogWarning("game manager construct");
            _resolver = resolver;
            _enemiesManager = _resolver.Resolve<EnemiesManager>();
            _playerModel = _resolver.Resolve<PlayerModel>();
            _uiManager = _resolver.Resolve<UIManager>();
        }

        private void Awake()
        {
            _playerModel.Health.Where(x => x <= 0).Subscribe(_ => GameOver()).AddTo(Disposables);
        }

        private void GameOver()
        {
            Debug.LogWarning("GAME OVER");
            _playerModel.ResetPlayer();
            _stateMachine.ChangeStateTo(UIType.GameOver);
        }

        public void ShowView(UIType viewType) => _uiManager.ShowView(viewType);
        public void HideView(UIType viewType) => _uiManager.HideView(viewType);

        public void StopTheGame()
        {
            _enemiesManager.StopTheGame();
            isGameStarted = false;
        }

        public void StartThegame()
        {
            _playerModel.NewGameStart();
            _enemiesManager.StartTheGame();
        }
    }
}
