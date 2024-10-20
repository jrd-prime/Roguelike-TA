using System;
using Game.Scripts.UI.Interfaces;
using VContainer;

namespace Game.Scripts.UI.Base
{
    public class UIViewCustom<T> : UIViewBase where T : IUIViewModel
    {
        protected T ViewModel;

        [Inject]
        private void Construct(T viewModel)
        {
            ViewModel = viewModel != null
                ? viewModel
                : throw new NullReferenceException($"ViewModel {typeof(T)} is null!");
        }
    }
}
