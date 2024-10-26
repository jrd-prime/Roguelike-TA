using Game.Scripts.UI.Base;
using R3;

namespace Game.Scripts.UI.Menus.MainMenu
{
    public class MenuUIViewModel : UIViewModelCustom<IMenuUIModel>
    {
        public Subject<Unit> StartButtonClicked { get; } = new();
        public Subject<Unit> SettingsButtonClicked { get; } = new();
        public Subject<Unit> ExitButtonClicked { get; } = new();


        public override void Initialize()
        {
            StartButtonClicked.Subscribe(_ => Model.StartButtonClicked()).AddTo(Disposables);
            SettingsButtonClicked.Subscribe(_ => Model.SettingsButtonClicked()).AddTo(Disposables);
            ExitButtonClicked.Subscribe(_ => Model.ExitButtonClicked()).AddTo(Disposables);
        }
    }
}
