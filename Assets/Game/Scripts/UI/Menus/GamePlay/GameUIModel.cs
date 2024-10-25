using Game.Scripts.Player;
using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Interfaces;
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

        private PlayerModel _playerModel;

        public override void Initialize()
        {
            _playerModel = Container.Resolve<PlayerModel>();

            PlayerHealth.Value = _playerModel.Health.Value;
            PlayerInitialHealth.Value = _playerModel.characterSettings.health;

            _playerModel.Health
                .Subscribe(health => PlayerHealth.Value = health)
                .AddTo(Disposables);

            // GamwManager.KillCount
            //     .Subscribe(killCount => KillCount.Value = killCount)
            //     .AddTo(Disposables);
            //
            // GamwManager.KillToWin
            //     .Subscribe(killToWin => KillToWin.Value = killToWin)
            //     .AddTo(Disposables);
        }


        public void MenuButtonClicked()
        {
            StateMachine.ChangeStateTo(StateType.Pause);
        }
    }

    public interface IGameUIModel : IUIModel
    {
        public ReactiveProperty<float> PlayerHealth { get; }
        public void MenuButtonClicked();
        public ReactiveProperty<float> PlayerInitialHealth { get; }
        public ReactiveProperty<int> KillCount { get; }
        public ReactiveProperty<int> KillToWin { get; }
    }
}
