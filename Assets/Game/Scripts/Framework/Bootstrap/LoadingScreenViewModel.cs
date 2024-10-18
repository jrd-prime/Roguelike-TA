using R3;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Bootstrap
{
    public class LoadingScreenViewModel : IViewModel
    {
        [Inject] private ILoadingScreenModel _model;

        public ReactiveProperty<string> HeaderView { get; } = new();

        public void Initialize()
        {
            Assert.IsNotNull(_model, $"{typeof(ILoadingScreenModel)} is null.");

            _model.LoadingText.Subscribe(x => HeaderView.Value = x);
        }
    }

    public interface IViewModel : IInitializable
    {
    }
}
