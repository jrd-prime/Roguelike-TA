using Game.Scripts.UI.Menus.Interfaces;
using R3;

namespace Game.Scripts.UI.Base
{
    public abstract class UIViewModelBase : IUIViewModel
    {
        protected readonly CompositeDisposable Disposables = new();

        public abstract void Initialize();
    }
}
