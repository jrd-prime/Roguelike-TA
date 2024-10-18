using Game.Scripts.Framework.GameStateMachine.State;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.GameStateMachine
{
    public class StateMachine : IInitializable
    {
        private IGameState _currentState;
        private IGameState _mainMenuState;

        [Inject]
        private void Construct(MainMenuState mainMenuState)
        {
            _mainMenuState = mainMenuState;
        }

        public void Initialize()
        {
            Debug.Log("State machine");
            if (_currentState != null) return;

            ChangeState(_mainMenuState);
            _currentState = _mainMenuState;
        }

        public void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}
