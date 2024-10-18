using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Framework.Providers.AssetProvider
{
    public sealed class AssetProvider : IAssetProvider
    {
        // todo refact
        public string Description => "AssetProvider";
        public Dictionary<string, Sprite> IconCache { get; } = new();
        public Dictionary<AssetReferenceTexture2D, Sprite> IconCache2 { get; } = new();

        public void ServiceInitialization()
        {
            Addressables.InitializeAsync();
        }

        public async UniTask<SceneInstance> LoadSceneAsync(string assetId, LoadSceneMode loadSceneMode)
        {
            return await Addressables.LoadSceneAsync(AssetConst.GameScene, loadSceneMode).Task;
        }

        public async UniTask<GameObject> LoadAssetAsync(string assetId)
        {
            var handle = await Addressables.LoadAssetAsync<GameObject>(assetId);
            return handle;
        }

        public async UniTask<GameObject> LoadAssetAsync(AssetReferenceGameObject assetReferenceGameObject)
        {
            return await Addressables.LoadAssetAsync<GameObject>(assetReferenceGameObject);
        }

        public async UniTask<GameObject> InstantiateAsync(string assetId, Transform parent = null)
        {
            var handle = Addressables.InstantiateAsync(assetId, parent);
            return await handle;
        }

        public async UniTask<GameObject> InstantiateAsync(AssetReference assetId, Vector3 fixedPositionValue)
        {
            // await CheckAsset(assetId);
            var handle = Addressables.InstantiateAsync(assetId);
            return await handle.Task;
        }

        public async UniTask<GameObject> InstantiateAsync(AssetReference assetId, Transform position)
        {
            // await CheckAsset(assetId);
            var handle = Addressables.InstantiateAsync(assetId, position);
            return await handle.Task;
        }

        public UniTask<Sprite> LoadIconAsync(string elementTypeName)
        {
            throw new NotImplementedException();
        }

        public Sprite GetIconFromRef(AssetReferenceTexture2D icon)
        {
            if (IconCache2.TryGetValue(icon, out Sprite iconFromCache)) return iconFromCache;

            Sprite iconNew = Addressables.LoadAssetAsync<Sprite>(icon).WaitForCompletion();

            IconCache2.TryAdd(icon, iconNew);

            return iconNew;
        }

        public Sprite GetIconFromName(string toString)
        {
            if (IconCache.TryGetValue(toString, out Sprite iconFromCache)) return iconFromCache;

            var iconNew = Addressables.LoadAssetAsync<Sprite>(toString).WaitForCompletion();

            IconCache.TryAdd(toString, iconNew);

            return iconNew;
        }
    }
}
