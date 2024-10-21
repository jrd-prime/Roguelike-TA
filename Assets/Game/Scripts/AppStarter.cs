using Cysharp.Threading.Tasks;
using Game.Scripts.Framework.Bootstrap;
using Game.Scripts.Framework.Providers.AssetProvider;
using Game.Scripts.Framework.Sort.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts
{
    public sealed class AppStarter : IInitializable
    {
        private ILoader _loader;
        private IAssetProvider _assetProvider;
        private IConfigManager _configManager;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _loader = container.Resolve<ILoader>();
            _assetProvider = container.Resolve<IAssetProvider>();
            _configManager = container.Resolve<IConfigManager>();
        }

        public async void Initialize()
        {
            _loader.AddServiceToInitialize(_configManager);
            _loader.AddServiceToInitialize(_assetProvider);


            Debug.LogWarning("Starting services initialization...");
            await _loader.StartServicesInitializationAsync();
            Debug.LogWarning("Services initialization completed...");


            var gameScene = await _assetProvider.LoadSceneAsync(AssetConst.GameScene, LoadSceneMode.Additive);

            // TODO FadeOut loading screen view 
            // Unloading current scene
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(gameScene.Scene);
            await SceneManager.UnloadSceneAsync(currentScene);
        }
    }
}
