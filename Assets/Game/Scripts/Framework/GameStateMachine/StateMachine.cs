using System.Collections.Generic;
using Game.Scripts.Framework.GameStateMachine.State;
using Game.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.GameStateMachine
{
    public class StateMachine : IPostStartable
    {
        private IGameState _currentState;

        private readonly Dictionary<UIType, IGameState> _states = new();

        [Inject]
        private void Construct(IObjectResolver container)
        {
            Debug.LogWarning("State machine construct");
            _states.Add(UIType.Menu, container.Resolve<MenuState>());
            _states.Add(UIType.GameOver, container.Resolve<GameOverState>());
            _states.Add(UIType.Pause, container.Resolve<PauseState>());
            _states.Add(UIType.Game, container.Resolve<GamePlayState>());
            _states.Add(UIType.Settings, container.Resolve<SettingsState>());
        }

        private void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void ChangeStateTo(UIType settings)
        {
            if (!_states.TryGetValue(settings, out IGameState state))
                throw new KeyNotFoundException($"State: {settings} not found!");

            ChangeState(state);
        }

        public void PostStart()
        {
            Debug.LogWarning("State machine post start");
            if (_currentState != null) return;

            ChangeState(_states[UIType.Menu]);
            _currentState = _states[UIType.Menu];
        }
    }
}
