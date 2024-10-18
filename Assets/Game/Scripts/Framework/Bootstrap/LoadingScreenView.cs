using Game.Scripts.UI;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Scripts.Framework.Bootstrap
{
    public class LoadingScreenView : MonoBehaviour
    {
        private LoadingScreenViewModel _viewModel;

        [Inject]
        private void Construct(LoadingScreenViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Awake()
        {
            var uiDocument = gameObject.GetComponent<UIDocument>();

            Assert.IsNotNull(uiDocument.visualTreeAsset, "VisualTreeAsset is not set to " + name + " prefab!");

            var header = uiDocument.rootVisualElement.Q<Label>("header-label");

            _viewModel.HeaderView.Subscribe(x => header.text = x);
        }
    }
}
