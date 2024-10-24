using Cysharp.Threading.Tasks;
using Game.Scripts.Framework.Configuration;
using Game.Scripts.Framework.Providers.AssetProvider;
using Game.Scripts.Framework.ScriptableObjects.Weapon;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Weapon
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected WeaponSettings settings;

        [SerializeField] public Transform muzzlePosition;

        private IConfigManager _configManager;
        private IAssetProvider _assetProvider;

        private CustomPool<Projectile> _projectilePool;

        [Inject]
        private void Construct(IConfigManager configManager, IAssetProvider assetProvider)
        {
            _configManager = configManager;
            _assetProvider = assetProvider;
        }

        private async void Awake()
        {
            Assert.IsNotNull(_configManager, $"Config manager is null. Add {this} to auto inject");
            Assert.IsNotNull(_assetProvider, $"Asset provider is null. Add {this} to auto inject");

            settings = _configManager.GetConfig<WeaponSettings>();
        }

        public void Shoot(Vector3 targetPosition, Projectile projectile)
        {
            Debug.LogWarning("Shoot");
            projectile.gameObject.SetActive(true);
            projectile.LaunchToTarget(muzzlePosition.position, new Vector3(targetPosition.x, .3f, targetPosition.z));
        }
    }
}
