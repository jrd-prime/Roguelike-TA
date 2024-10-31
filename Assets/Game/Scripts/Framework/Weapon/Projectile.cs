using System;
using Game.Scripts.Enemy;
using Game.Scripts.Framework.Configuration.SO.Weapon;
using Game.Scripts.Framework.Managers.Settings;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Weapon
{
    [RequireComponent(typeof(SphereCollider))]
    public class Projectile : MonoBehaviour, IProjectile
    {
        public float speed = 100f;
        public float damage = 10f;
        private Vector3 _targetPosition;
        private bool _isMoving;
        private bool _hasHit;

        private WeaponSettings _weaponSettings;
        private Action<Projectile> _callback;

        [Inject]
        private void Construct(ISettingsManager settingsManager)
        {
            _weaponSettings = settingsManager.GetConfig<WeaponSettings>();
            Assert.IsNotNull(_weaponSettings, "Weapon config is null.");

            damage = _weaponSettings.projectileDamage;
            speed = _weaponSettings.projectileSpeed;
        }

        public void SetPoolCallback(WeaponSettings weaponSettings, Action<Projectile> poolCallback)
        {
            Assert.IsNotNull(poolCallback, "ProjectilePool callback is not set");
            _callback = poolCallback;
        }

        private void FixedUpdate()
        {
            if (!_isMoving) return;
            MoveToTarget();
        }

        public void LaunchToTarget(Vector3 from, Vector3 to)
        {
            Assert.IsNotNull(_callback, "Pool callback must be set before launching.");

            _targetPosition = to;
            transform.position = from;
            _isMoving = true;
            _hasHit = false;
        }

        private void MoveToTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
            transform.LookAt(_targetPosition);

            if (transform.position != _targetPosition || _hasHit) return;
            _hasHit = true;
            _callback?.Invoke(this);
            _isMoving = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;

            _isMoving = false;
            _hasHit = true;

            var enemy = other.GetComponent<EnemyHolder>();

            if (enemy == null) return;

            enemy.TakeDamage(damage);
            _callback.Invoke(this);
        }
    }

    public interface IProjectile
    {
        public void LaunchToTarget(Vector3 from, Vector3 to);
        public void SetPoolCallback(WeaponSettings weaponSettings, Action<Projectile> poolCallback);
    }
}
