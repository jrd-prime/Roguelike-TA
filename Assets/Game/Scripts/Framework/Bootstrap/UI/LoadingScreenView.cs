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
        private LoadingScreenUIViewModel _model;
        private Label _title;

        private readonly CompositeDisposable _disposables = new();

        private const string TitleLabelId = "header-label";

        [Inject]
        private void Construct(LoadingScreenUIViewModel uiModel) => _model = uiModel;

        private void Awake()
        {
            Assert.IsNotNull(_model, "ViewModel is null");

            var uiDocument = gameObject.GetComponent<UIDocument>();
            Assert.IsNotNull(uiDocument.visualTreeAsset, "VisualTreeAsset is not set to " + name + " prefab!");

            _title = uiDocument.rootVisualElement.Q<Label>(TitleLabelId);
            Assert.IsNotNull(_title, $"Can't find label with id {TitleLabelId} in {uiDocument.name}");

            _model.TitleText.Subscribe(SetTitle).AddTo(_disposables);
        }

        private void SetTitle(string value)
        {
            if (string.IsNullOrEmpty(value)) _title.text = "Not set";
            _title.text = value;
        }

        private void OnDestroy() => _disposables?.Dispose();
    }
}
