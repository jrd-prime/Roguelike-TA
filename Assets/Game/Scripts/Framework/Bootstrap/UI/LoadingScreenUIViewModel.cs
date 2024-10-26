using R3;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Bootstrap.UI
{
    public class LoadingScreenUIViewModel : IInitializable
    {
        [Inject] private ILoadingScreenModel _model;
        private readonly CompositeDisposable _disposables = new();

        public ReactiveProperty<string> TitleText { get; } = new();

        public void Initialize()
        {
            Assert.IsNotNull(_model, $"{typeof(ILoadingScreenModel)} is null.");

            _model.LoadingText
                .Subscribe(titleText => TitleText.Value = titleText)
                .AddTo(_disposables);
        }
    }
}
