using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Models;
using R3;

namespace Game.Scripts.UI.Menus.ViewModels
{
    public class GameOverUIViewModel : UIViewModelCustom<IGameOverUIModel>
    {
        public Subject<Unit> MenuButtonClicked { get; } = new();
        public Subject<Unit> NewGameButtonClicked { get; } = new();
        public Subject<Unit> SettingsButtonClicked { get; } = new();

        public override void Initialize()
        {
            SettingsButtonClicked.Subscribe(_ => Model.SettingsButtonClicked()).AddTo(Disposables);
            NewGameButtonClicked.Subscribe(_ => Model.NewGameButtonClicked()).AddTo(Disposables);
            MenuButtonClicked.Subscribe(_ => Model.MenuButtonClicked()).AddTo(Disposables);
        }
    }
}
