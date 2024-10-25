using Game.Scripts.UI.Base;
using R3;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public class GameUIViewModel : UIViewModelCustom<IGameUIModel>
    {
        public Subject<Unit> MenuButtonClicked { get; } = new();
        public ReadOnlyReactiveProperty<float> PlayerHealth => Model.PlayerHealth;
        public ReadOnlyReactiveProperty<float> PlayerInitialHealth => Model.PlayerInitialHealth;
        public ReadOnlyReactiveProperty<int> KillCount => Model.KillCount;
        public ReadOnlyReactiveProperty<int> KillToWin => Model.KillToWin;

        public override void Initialize()
        {
            MenuButtonClicked.Subscribe(_ => Model.MenuButtonClicked()).AddTo(Disposables);
        }
    }
}
