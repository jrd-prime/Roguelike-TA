using Game.Scripts.UI;
using R3;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Scripts.Framework.Bootstrap
{
    public class LoadingScreenView : UIView
    {
        private LoadingScreenViewModel _viewModel;

        [Inject]
        private void Construct(LoadingScreenViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Start()
        {
            var header = RootVisualElement.Q<Label>("header-label");

            _viewModel.HeaderView.Subscribe(x => header.text = x);
        }
    }
}
