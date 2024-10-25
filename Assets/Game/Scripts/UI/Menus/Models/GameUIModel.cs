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
        public ReactiveProperty<float> PlayerInitialHealth { get; } = new();
        private PlayerModel _playerModel;

        public override void Initialize()
        {
            _playerModel = Container.Resolve<PlayerModel>();

            PlayerHealth.Value = _playerModel.Health.Value;
            PlayerInitialHealth.Value = _playerModel.characterSettings.health;

            _playerModel.Health
                .Subscribe(health => PlayerHealth.Value = health)
                .AddTo(Disposables);
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
        public ReactiveProperty<float> PlayerInitialHealth { get; }
    }
}
