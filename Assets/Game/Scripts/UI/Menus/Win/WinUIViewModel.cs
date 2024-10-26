using Game.Scripts.UI.Base;
using R3;

namespace Game.Scripts.UI.Menus.Win
{
    public class WinUIViewModel : UIViewModelCustom<IWinUIModel>
    {
        public Subject<Unit> StartButtonClicked { get; } = new();
        public Subject<Unit> ExitButtonClicked { get; } = new();
        public Subject<Unit> MenuButtonClicked { get; } = new();

        public override void Initialize()
        {
            StartButtonClicked.Subscribe(_ => Model.StartButtonClicked()).AddTo(Disposables);
            ExitButtonClicked.Subscribe(_ => Model.ExitButtonClicked()).AddTo(Disposables);
            MenuButtonClicked.Subscribe(_ => Model.MenuButtonClicked()).AddTo(Disposables);
        }
    }
}
