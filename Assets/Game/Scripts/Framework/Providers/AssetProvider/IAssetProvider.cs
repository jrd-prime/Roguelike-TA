using Cysharp.Threading.Tasks;
using Game.Scripts.Framework.Bootstrap;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Framework.Providers.AssetProvider
{
    public interface IAssetProvider : ILoadingOperation
    {
        public UniTask<SceneInstance> LoadSceneAsync(string assetId, LoadSceneMode loadSceneMode);
        public UniTask<GameObject> InstantiateAsync(AssetReference assetId, Transform parent = null);
    }
}
