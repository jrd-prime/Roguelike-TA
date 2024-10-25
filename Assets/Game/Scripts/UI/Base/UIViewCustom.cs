using System;
using Game.Scripts.UI.Menus.Interfaces;
using VContainer;

namespace Game.Scripts.UI.Base
{
    public abstract class UIViewCustom<T> : UIViewBase where T : IUIViewModel
    {
        protected T ViewModel;

        [Inject]
        private void Construct(T viewModel)
        {
            ViewModel = viewModel;
        }

        private void Start()
        {
            if (ViewModel == null) throw new NullReferenceException($"ViewModel in {name} is null. Check container registration!");
        }
    }
}
