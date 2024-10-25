using System.Collections.Generic;
using Game.Scripts.Framework.GameStateMachine.State;
using Game.Scripts.Player;
using Game.Scripts.UI;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.GameStateMachine
{
    public class StateMachine : IPostStartable, IInitializable
    {
        private readonly Dictionary<StateType, IGameState> _states = new();
        private readonly CompositeDisposable _disposables = new();

        private IGameState _currentState;
        private PlayerModel _playerModel;


        [Inject]
        private void Construct(IObjectResolver container)
        {
            Debug.LogWarning("State machine construct");
            _states.Add(StateType.Menu, container.Resolve<MenuState>());
            _states.Add(StateType.GameOver, container.Resolve<GameOverState>());
            _states.Add(StateType.Pause, container.Resolve<PauseState>());
            _states.Add(StateType.Game, container.Resolve<GamePlayState>());
            _states.Add(StateType.Settings, container.Resolve<SettingsState>());
            _states.Add(StateType.Win, container.Resolve<WinState>());

            _playerModel = container.Resolve<PlayerModel>();
        }

        private void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void ChangeStateTo(StateType stateType)
        {
            if (!_states.TryGetValue(stateType, out IGameState state))
                throw new KeyNotFoundException($"State: {stateType} not found!");

            ChangeState(state);
        }

        public void PostStart()
        {
            Debug.LogWarning("State machine post start");
            if (_currentState != null) return;

            ChangeState(_states[StateType.Menu]);
            _currentState = _states[StateType.Menu];
        }

        public void Initialize()
        {
            _playerModel.Health.Where(x => x <= 0).Subscribe(_ => ChangeStateTo(StateType.GameOver))
                .AddTo(_disposables);
        }
    }
}
