using Cysharp.Threading.Tasks;
using Game.Scripts.Framework.Bootstrap;
using Game.Scripts.Framework.Providers.AssetProvider;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts
{
    public sealed class AppStarter : IInitializable
    {
        private ILoader _loader;
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _loader = container.Resolve<ILoader>();
            _assetProvider = container.Resolve<IAssetProvider>();
        }

        public async void Initialize()
        {
            UniTask<SceneInstance> gameSceneTask =
                _assetProvider.LoadSceneAsync(AssetConst.GameScene, LoadSceneMode.Additive);
            
            _loader.AddServiceToInitialize(_assetProvider);
            
            var gameScene = await gameSceneTask;
            
            await _loader.StartServicesInitializationAsync();


            // TODO FadeOut loading screen view 
            // Unloading current scene
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(gameScene.Scene);
            await SceneManager.UnloadSceneAsync(currentScene);
        }
    }
}
