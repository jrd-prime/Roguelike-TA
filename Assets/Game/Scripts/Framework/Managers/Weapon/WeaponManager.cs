using Cysharp.Threading.Tasks;
using Game.Scripts.Framework.Configuration.SO.Weapon;
using Game.Scripts.Framework.Helpers;
using Game.Scripts.Framework.Providers.AssetProvider;
using Game.Scripts.Framework.Providers.Pools;
using Game.Scripts.Framework.Weapon;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Managers.Weapon
{
    public class WeaponManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private WeaponSettings charWeaponSettings;
        [SerializeField] public WeaponBase characterWeapon;
        [SerializeField] private AssetReferenceGameObject projectileAssetReference;


        private IAssetProvider _assetProvider;
        private IObjectResolver _resolver;
        private CustomPool<Projectile> _projectilePool;

        [Inject]
        private void Construct(IObjectResolver resolver) => _resolver = resolver;

        public void Initialize()
        {
            _assetProvider = Resolver.ResolveAndCheck<IAssetProvider>(_resolver);
            Assert.IsNotNull(charWeaponSettings, "charWeaponSettings is null");
            Assert.IsNotNull(projectileAssetReference, "projectileAssetReference is null");
            Assert.IsNotNull(characterWeapon, "charWeapon is null");
        }

        private async UniTask<Projectile> GetProjectile()
        {
            var projectileObj = await _assetProvider.InstantiateAsync(projectileAssetReference);
            projectileObj.SetActive(false);
            return projectileObj.GetComponent<Projectile>();
        }

        public async UniTask<WeaponBase> GetCharacterWeapon()
        {
            var projectile = await GetProjectile();
            _projectilePool = new CustomPool<Projectile>(projectile, 100, null, _resolver);
            characterWeapon.InitializeWeapon(charWeaponSettings, _projectilePool);

            return characterWeapon;
        }
    }
}
