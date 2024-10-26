using R3;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Bootstrap.UI
{
    public class LoadingScreenViewModel : ILoadingScreenViewModel
    {
        public ReactiveProperty<string> TitleText => _model.LoadingText;

        private ILoadingScreenModel _model;

        [Inject]
        private void Construct(ILoadingScreenModel loadingScreenModel) => _model = loadingScreenModel;

        public void Initialize()
        {
            Assert.IsNotNull(_model, $"{typeof(ILoadingScreenModel)} is null.");
        }
    }
}
