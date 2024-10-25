using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Models;
using R3;

namespace Game.Scripts.UI.Menus.ViewModels
{
    public class GameUIViewModel : UIViewModelCustom<IGameUIModel>
    {
        public Subject<Unit> MenuButtonClicked { get; } = new();
        public ReadOnlyReactiveProperty<float> PlayerHealth => Model.PlayerHealth;
        public ReadOnlyReactiveProperty<float> PlayerInitialHealth => Model.PlayerInitialHealth;

        public override void Initialize()
        {
            MenuButtonClicked.Subscribe(_ => Model.MenuButtonClicked()).AddTo(Disposables);
        }
    }
}
