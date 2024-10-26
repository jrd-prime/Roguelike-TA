using R3;
using VContainer.Unity;

namespace Game.Scripts.Framework.Bootstrap.UI
{
    public interface ILoadingScreenViewModel : IInitializable
    {
        public ReactiveProperty<string> TitleText { get; }
    }
}
