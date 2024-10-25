using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Scripts.Framework.Bootstrap
{
    public sealed class Loader : ILoader, ILoadingScreenModel
    {
        private readonly Queue<ILoadingOperation> _loadingQueue = new();

        public ReactiveProperty<string> LoadingText { get; } = new("Default");

        public void AddServiceToInitialize(ILoadingOperation service)
        {
            Assert.IsNotNull(service, "Service is null!");
            _loadingQueue.Enqueue(service);
        }

        public async UniTask StartServicesInitializationAsync()
        {
            foreach (var service in _loadingQueue)
            {
                try
                {
                    Debug.LogWarning($"Initialize {service.GetType().Name}...");
                    LoadingText.Value = $"(+ fake delayed) Loading: {service.Description}..";
                    service.LoaderServiceInitialization();

                    // fake delay per service
                    await UniTask.Delay(1000);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to initialize {service.GetType().Name}: {ex.Message}");
                }
            }

            await UniTask.CompletedTask;
        }
    }
}
