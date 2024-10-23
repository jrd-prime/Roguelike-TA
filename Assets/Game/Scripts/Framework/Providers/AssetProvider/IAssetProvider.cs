using System.Collections.Generic;
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
        // todo refact
        public Dictionary<string, Sprite> IconCache { get; }
        public UniTask<SceneInstance> LoadSceneAsync(string assetId, LoadSceneMode loadSceneMode);
        public UniTask<GameObject> LoadAssetAsync(string assetId);
        public UniTask<GameObject> LoadAssetAsync(AssetReferenceGameObject assetReferenceGameObject);
        public UniTask<GameObject> InstantiateAsync(string assetId, Transform parent = null);
        public UniTask<GameObject> InstantiateAsync(AssetReference assetId, Vector3 fixedPositionValue);
        public UniTask<GameObject> InstantiateAsync(AssetReference assetId, Transform parent = null);
        public UniTask<Sprite> LoadIconAsync(string elementTypeName);
        public Sprite GetIconFromRef(AssetReferenceTexture2D icon);
    }
}
