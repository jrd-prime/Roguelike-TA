using System;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Scripts.Framework.Bootstrap.UI
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class LoadingScreenView : MonoBehaviour
    {
        private ILoadingScreenViewModel _viewModel;
        private Label _title;

        private readonly CompositeDisposable _disposables = new();

        private const string TitleLabelId = "header-label";

        [Inject]
        private void Construct(ILoadingScreenViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            Assert.IsNotNull(_viewModel, "ViewModel is null");

            var uiDocument = gameObject.GetComponent<UIDocument>();
            Assert.IsNotNull(uiDocument.visualTreeAsset, "VisualTreeAsset is not set to " + name + " prefab!");

            _title = uiDocument.rootVisualElement.Q<Label>(TitleLabelId);
            Assert.IsNotNull(_title, $"Can't find label with id {TitleLabelId} in {uiDocument.name}");

            _viewModel.TitleText.Subscribe(SetTitle).AddTo(_disposables);
        }

        private void SetTitle(string value)
        {
            if (string.IsNullOrEmpty(value)) _title.text = "Not set";
            _title.text = value;
        }

        private void OnDestroy() => _disposables?.Dispose();
    }
}
