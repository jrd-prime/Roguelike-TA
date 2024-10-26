using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Framework.Bootstrap.UI;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Bootstrap
{
    public class Loader : ILoader
    {
        private readonly Queue<ILoadingOperation> _loadingQueue = new();
        private ILoadingScreenModel _loadingScreenModel;

        [Inject]
        private void Construct(ILoadingScreenModel loadingScreenModel) => _loadingScreenModel = loadingScreenModel;


        public void AddServiceForInitialization(ILoadingOperation service)
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
                    _loadingScreenModel.SetLoadingText($"(+ fake delayed) Loading: {service.Description}..");
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
