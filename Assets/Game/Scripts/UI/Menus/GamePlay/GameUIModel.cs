using Game.Scripts.Player;
using Game.Scripts.Player.Interfaces;
using Game.Scripts.UI.Base;
using R3;
using VContainer;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public class GameUIModel : UIModelBase, IGameUIModel
    {
        public ReactiveProperty<float> PlayerHealth { get; } = new();
        public ReactiveProperty<float> PlayerInitialHealth { get; } = new();
        public ReactiveProperty<int> KillCount => GamwManager.KillCount;
        public ReactiveProperty<int> KillToWin => GamwManager.KillToWin;

        private IPlayerModel _playerModel;

        public override void Initialize()
        {
            _playerModel = Container.Resolve<IPlayerModel>();

            PlayerHealth.Value = _playerModel.Health.Value;
            PlayerInitialHealth.Value = _playerModel.characterSettings.health;

            _playerModel.Health
                .Subscribe(health => PlayerHealth.Value = health)
                .AddTo(Disposables);
        }


        public void MenuButtonClicked()
        {
            StateMachine.ChangeStateTo(StateType.Pause);
        }
    }
}
