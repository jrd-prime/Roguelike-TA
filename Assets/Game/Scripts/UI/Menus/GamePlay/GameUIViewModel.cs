using Game.Scripts.UI.Base;
using R3;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public class GameUIViewModel : UIViewModelCustom<GameUIModel>
    {
        public Subject<Unit> MenuButtonClicked { get; } = new();
        public ReadOnlyReactiveProperty<int> PlayerHealth => Model.PlayerHealth;
        public ReadOnlyReactiveProperty<int> PlayerInitialHealth => Model.PlayerInitialHealth;
        public ReadOnlyReactiveProperty<int> KillCount => Model.KillCount;
        public ReadOnlyReactiveProperty<int> KillToWin => Model.KillToWin;
        public ReadOnlyReactiveProperty<int> EnemiesCount => Model.EnemiesCount;
        public ReadOnlyReactiveProperty<int> PlayerLevel => Model.PlayerLevel;
        public ReadOnlyReactiveProperty<int> PlayerExp => Model.CurrentExp;
        public ReadOnlyReactiveProperty<int> ExpToNextLevel => Model.ExpToNextLevel;

        public override void Initialize()
        {
            MenuButtonClicked.Subscribe(_ => Model.MenuButtonClicked()).AddTo(Disposables);
        }
    }
}
