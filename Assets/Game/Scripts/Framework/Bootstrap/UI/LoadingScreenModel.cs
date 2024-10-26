using System;
using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Bootstrap.UI
{
    public class LoadingScreenModel : ILoadingScreenModel
    {
        public ReactiveProperty<string> LoadingText { get; } = new();

        public void SetLoadingText(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("value can't be null", nameof(value));

            Debug.LogWarning("Set loading text");
            LoadingText.Value = value;
        }

        public void Dispose()
        {
            LoadingText?.Dispose();
        }
    }
}
