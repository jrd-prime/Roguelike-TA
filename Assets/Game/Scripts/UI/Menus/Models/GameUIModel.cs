using Game.Scripts.Framework.Configuration;
using Game.Scripts.Player;
using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Interfaces;
using R3;
using UnityEngine;
using VContainer;

namespace Game.Scripts.UI.Menus.Models
{
    public class GameUIModel : UIModelBase, IGameUIModel
    {
        public ReactiveProperty<float> PlayerHealth { get; } = new();

        private PlayerModel _playerModel;

        public override void Initialize()
        {
            _playerModel = Container.Resolve<PlayerModel>();

            PlayerHealth.Value = _playerModel.Health.Value;

            // Subscribe to player health changes
            _playerModel.Health.Subscribe(health =>
            {
                if (PlayerHealth.Value <= 0)
                {
                    Debug.LogWarning("Game ui model => GAME OVER");
                    StateMachine.ChangeStateTo(UIType.GameOver);
                    return;
                }


                PlayerHealth.Value = health;
            }).AddTo(Disposables);
        }


        public void MenuButtonClicked()
        {
            StateMachine.ChangeStateTo(UIType.Pause);
        }
    }

    public interface IGameUIModel : IUIModel
    {
        public ReactiveProperty<float> PlayerHealth { get; }
        public void MenuButtonClicked();
    }
}
