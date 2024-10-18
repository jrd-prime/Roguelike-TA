using R3;

namespace Game.Scripts.Framework.Bootstrap
{
    public interface ILoadingScreenModel
    {
        public ReactiveProperty<string> LoadingText { get; }
    }
}
