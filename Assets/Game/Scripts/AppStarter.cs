using Cysharp.Threading.Tasks;
using Game.Scripts.Framework.Bootstrap;
using Game.Scripts.Framework.Constants;
using Game.Scripts.Framework.Managers.Settings;
using Game.Scripts.Framework.Providers.AssetProvider;
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
        private ISettingsManager _settingsManager;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _loader = container.Resolve<ILoader>();
            _assetProvider = container.Resolve<IAssetProvider>();
            _settingsManager = container.Resolve<ISettingsManager>();
        }

        public async void Initialize()
        {
            _loader.AddServiceForInitialization(_settingsManager);
            _loader.AddServiceForInitialization(_assetProvider);


            Debug.LogWarning("Starting services initialization...");
            await _loader.StartServicesInitializationAsync();
            Debug.LogWarning("Services initialization completed...");


            var gameScene = await _assetProvider.LoadSceneAsync(AssetsConst.GameScene, LoadSceneMode.Additive);

            // TODO FadeOut loading screen view 
            // Unloading current scene
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(gameScene.Scene);
            await SceneManager.UnloadSceneAsync(currentScene);
        }
    }
}
