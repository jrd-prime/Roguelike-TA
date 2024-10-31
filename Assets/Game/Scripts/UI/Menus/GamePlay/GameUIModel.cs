using Game.Scripts.Player.Interfaces;
using Game.Scripts.UI.Base;
using R3;
using VContainer;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public class GameUIModel : UIModelBase, IGameUIModel
    {
        public ReactiveProperty<int> PlayerHealth { get; } = new();
        public ReactiveProperty<int> PlayerInitialHealth { get; } = new();
        public ReadOnlyReactiveProperty<int> KillCount => GameManager.KillCount;
        public ReadOnlyReactiveProperty<int> KillToWin => GameManager.KillToWin;
        public ReadOnlyReactiveProperty<int> EnemiesCount => GameManager.EnemiesCount;
        public ReadOnlyReactiveProperty<int> CurrentExp => GameManager.PlayerExp;
        public ReadOnlyReactiveProperty<int> ExpToNextLevel => GameManager.ExpToNextLevel;
        public ReadOnlyReactiveProperty<int> PlayerLevel => GameManager.PlayerLevel;

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

        public void MenuButtonClicked() => StateMachine.ChangeStateTo(StateType.Pause);
    }
}
