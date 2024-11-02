using System;
using R3;

namespace Game.Scripts.Framework.Bootstrap.UI
{
    public class LoadingScreenModel : ILoadingScreenModel
    {
        public ReactiveProperty<string> LoadingText { get; } = new();

        public void SetLoadingText(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("value can't be null", nameof(value));
            LoadingText.Value = value;
        }

        public void Dispose() => LoadingText?.Dispose();
    }
}
