using System;
using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.GameStateMachine.State;
using Game.Scripts.UI.Interfaces;
using UnityEngine;
using VContainer;

namespace Game.Scripts.UI.Models
{
    public interface IMenuUIModel : IUIModel
    {
        public void StartButtonClicked();
        public void SettingsButtonClicked();
        public void ExitButtonClicked();
    }

    public class MenuUIModel : IMenuUIModel
    {
        private StateMachine _stateMachine;
        private GamePlayState _gamePlayState;

        [Inject]
        private void Construct(StateMachine stateMachine, GamePlayState gamePlayState)
        {
            _stateMachine = stateMachine;
            _gamePlayState = gamePlayState;
        }

        public void StartButtonClicked()
        {
            Debug.LogWarning("Start button clicked model receive");
            _stateMachine.ChangeState(_gamePlayState);
            throw new NotImplementedException();
        }

        public void SettingsButtonClicked()
        {
            throw new NotImplementedException();
        }

        public void ExitButtonClicked()
        {
            throw new NotImplementedException();
        }
    }
}
