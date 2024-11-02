using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Framework.Bootstrap.UI;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Bootstrap
{
    public class Loader : ILoader, IInitializable
    {
        private readonly Queue<ILoadingOperation> _loadingQueue = new();
        private ILoadingScreenModel _loadingScreenModel;

        [Inject]
        private void Construct(ILoadingScreenModel loadingScreenModel) => _loadingScreenModel = loadingScreenModel;

        public void AddServiceForInitialization(ILoadingOperation service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            _loadingQueue.Enqueue(service);
        }

        public async UniTask StartServicesInitializationAsync()
        {
            if (_loadingQueue.Count == 0)
                throw new Exception("No services to initialize! Use AddServiceForInitialization first.");

            foreach (var service in _loadingQueue)
            {
                try
                {
                    _loadingScreenModel.SetLoadingText($"(+ fake delayed) Loading: {service.Description}..");
                    service.LoaderServiceInitialization();

                    // fake delay per service
                    await UniTask.Delay(100);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to initialize {service.GetType().Name}: {ex.Message}");
                }
            }

            await UniTask.CompletedTask;
        }

        public void Initialize()
        {
            if (_loadingScreenModel == null) throw new NullReferenceException($"LoadingScreenModel is null. {this}");
        }
    }
}
