using System;
using System.Collections;
using Game.Scripts.Enemy;
using Game.Scripts.Framework.Configuration.SO.Weapon;
using Game.Scripts.Framework.Managers.Settings;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using VContainer;

namespace Game.Scripts.Framework.Weapon
{
    [RequireComponent(typeof(SphereCollider))]
    public class ProjectileHolder : MonoBehaviour, IProjectile
    {
        public float speed = 100f;
        public float damage = 10f;
        public ParticleSystem explosion;
        public GameObject projectilePrefab;
        private Vector3 _targetPosition;
        private bool _isMoving;
        private bool _hasHit;
        private SphereCollider _sphereCollider;

        private WeaponSettings _weaponSettings;
        private Action<ProjectileHolder> _callback;

        [Inject]
        private void Construct(ISettingsManager settingsManager)
        {
            _weaponSettings = settingsManager.GetConfig<WeaponSettings>();
            Assert.IsNotNull(_weaponSettings, "Weapon config is null.");

            damage = _weaponSettings.projectileDamage;
            speed = _weaponSettings.projectileSpeed;

            _sphereCollider = GetComponent<SphereCollider>();
        }

        public void SetPoolCallback(WeaponSettings weaponSettings, Action<ProjectileHolder> poolCallback)
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


            _sphereCollider.enabled = false;
            projectilePrefab.gameObject.SetActive(false);
            StartCoroutine(ExplosionDelay());
            _isMoving = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;
            _isMoving = false;
            _hasHit = true;

            var enemy = other.GetComponent<EnemyHolder>();

            if (enemy == null) return;
            DeactivateProjectile();
            StartCoroutine(ExplosionDelay());
            enemy.TakeDamage(damage);
        }

        private void DeactivateProjectile()
        {
            _sphereCollider.enabled = false;
            projectilePrefab.gameObject.SetActive(false);
        }

        private IEnumerator ExplosionDelay()
        {
            explosion.Clear();
            explosion.Play();
            yield return new WaitForSeconds(1f);
            explosion.Stop();
            ActivateProjectile();
            _callback.Invoke(this);
        }

        private void ActivateProjectile()
        {
            _sphereCollider.enabled = true;
            projectilePrefab.gameObject.SetActive(true);
        }
    }

    public interface IProjectile
    {
        public void LaunchToTarget(Vector3 from, Vector3 to);
        public void SetPoolCallback(WeaponSettings weaponSettings, Action<ProjectileHolder> poolCallback);
    }
}
