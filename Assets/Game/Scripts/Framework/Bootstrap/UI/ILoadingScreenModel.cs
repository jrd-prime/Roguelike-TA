using System;
using R3;

namespace Game.Scripts.Framework.Bootstrap.UI
{
    public interface ILoadingScreenModel : IDisposable
    {
        public ReactiveProperty<string> LoadingText { get; }
        public void SetLoadingText(string value);
    }
}
