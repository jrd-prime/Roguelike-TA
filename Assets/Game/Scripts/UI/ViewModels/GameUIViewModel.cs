using Game.Scripts.UI.Base;
using Game.Scripts.UI.Models;
using R3;

namespace Game.Scripts.UI.ViewModels
{
    public class GameUIViewModel : UIViewModelCustom<IGameUIModel>
    {
        public Subject<Unit> MenuButtonClicked { get; } = new();

        public override void Initialize()
        {
           MenuButtonClicked.Subscribe(_ => Model.MenuButtonClicked()).AddTo(Disposables);
        }
    }
}
