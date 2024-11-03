using System;
using System.Collections;
using Game.Scripts.Framework.Configuration.SO.Weapon;
using Game.Scripts.Framework.Providers.Pools;
using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Weapon
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] public Transform muzzlePosition;
        public ReactiveProperty<bool> IsShooting { get; } = new(false);

        private WeaponSettings _weaponSettings;
        private CustomPool<ProjectileHolder> _projectilePool;
        private const float ShootHeightOffset = .3f;
        private bool _isInitialized;

        public void InitializeWeapon(WeaponSettings weaponSettings, CustomPool<ProjectileHolder> projectilePool)
        {
            _weaponSettings = weaponSettings;
            _projectilePool = projectilePool;
            _isInitialized = true;
        }

        private void PoolCallback(ProjectileHolder projectileHolder) => _projectilePool.Return(projectileHolder);

        public void ShootAtTarget(GameObject nearestEnemy)
        {
            if (!_isInitialized) throw new Exception("Weapon is not initialized!");

            var projectile = _projectilePool.Get();
            projectile.SetPoolCallback(_weaponSettings, PoolCallback);

            Shoot(nearestEnemy.transform.position, projectile);
        }

        private void Shoot(Vector3 targetPosition, ProjectileHolder projectileHolder)
        {
            Shoot(true);
            projectileHolder.gameObject.SetActive(true);
            projectileHolder.LaunchToTarget(muzzlePosition.position,
                new Vector3(targetPosition.x, ShootHeightOffset, targetPosition.z));
            StartCoroutine(ShootingDelay());
        }

        private IEnumerator ShootingDelay()
        {
            yield return new WaitForSeconds(_weaponSettings.attackDelayMS / 1000f);
            Shoot(false);
        }

        private void Shoot(bool value) => IsShooting.Value = value;
    }
}
