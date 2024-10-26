using System;
using System.Collections.Generic;
using Game.Scripts.Framework.GameStateMachine.State;
using Game.Scripts.Framework.Managers.Game;
using Game.Scripts.Player.Interfaces;
using Game.Scripts.UI;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.GameStateMachine
{
    public class StateMachine : IPostStartable, IDisposable
    {
        private readonly Dictionary<StateType, IGameState> _states = new();

        private IGameState _currentState;
        private IPlayerModel _playerModel;
        private GameManager _gameManager;
        private readonly CompositeDisposable _disposables = new();
        private bool isGameStarted;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _states.Add(StateType.Menu, container.Resolve<MenuState>());
            _states.Add(StateType.GameOver, container.Resolve<GameOverState>());
            _states.Add(StateType.Pause, container.Resolve<PauseState>());
            _states.Add(StateType.Game, container.Resolve<GamePlayState>());
            _states.Add(StateType.Settings, container.Resolve<SettingsState>());
            _states.Add(StateType.Win, container.Resolve<WinState>());

            _playerModel = container.Resolve<IPlayerModel>();
            _gameManager = container.Resolve<GameManager>();
        }

        public void PostStart()
        {
            Debug.LogWarning("state machine post start");
            Debug.LogWarning($"current state: {_currentState}");
            if (_currentState != null) return;

            ChangeState(_states[StateType.Menu]);
            _currentState = _states[StateType.Menu];

            _gameManager.isGameStarted
                .Subscribe(value => isGameStarted = value).AddTo(_disposables);

            _playerModel.Health
                .Where(h => h <= 0)
                .Subscribe(h =>
                {
                    if (isGameStarted) ChangeStateTo(StateType.GameOver);
                })
                .AddTo(_disposables);
        }

        public void ChangeStateTo(StateType stateType)
        {
            Debug.LogWarning($"change state to: {stateType}");
            if (!_states.TryGetValue(stateType, out IGameState state))
                throw new KeyNotFoundException($"State: {stateType} not found!");

            ChangeState(state);
        }

        private void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
