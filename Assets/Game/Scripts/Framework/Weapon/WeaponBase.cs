using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Framework.Configuration.SO.Weapon;
using Game.Scripts.Framework.Providers.Pools;
using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Weapon
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] public Transform muzzlePosition;
        public ReactiveProperty<bool> isShooting { get; } = new(false);

        private WeaponSettings _weaponSettings;
        private CustomPool<Projectile> _projectilePool;
        private const float ShootHeightOffset = .3f;
        private bool _isInitialized;

        public void InitializeWeapon(WeaponSettings weaponSettings, CustomPool<Projectile> projectilePool)
        {
            _weaponSettings = weaponSettings;
            _projectilePool = projectilePool;
            _isInitialized = true;
        }

        private void Shoot(Vector3 targetPosition, Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
            projectile.LaunchToTarget(muzzlePosition.position,
                new Vector3(targetPosition.x, ShootHeightOffset, targetPosition.z));
        }

        private void PoolCallback(Projectile projectile) => _projectilePool.Return(projectile);


        public async UniTask ShootAtTarget(GameObject nearestEnemy)
        {
            if (!_isInitialized) throw new Exception("Weapon is not initialized!");

            Shooting(true);

            var projectile = _projectilePool.Get();
            projectile.Initialize(_weaponSettings, PoolCallback);

            Shoot(nearestEnemy.transform.position, projectile);

            await UniTask.Delay(_weaponSettings.attackDelayMS);

            Shooting(false);
        }

        private void Shooting(bool value) => isShooting.Value = value;
    }
}
