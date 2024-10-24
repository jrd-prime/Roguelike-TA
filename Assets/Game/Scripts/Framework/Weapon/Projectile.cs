using System;
using Game.Scripts.Enemy;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Weapon
{
    [RequireComponent(typeof(SphereCollider))]
    public class Projectile : MonoBehaviour
    {
        public float speed = 100f; // Скорость движения снаряда

        private Vector3 _targetPosition; // Точка назначения
        private bool _isMoving; // Флаг, показывающий, движется ли снаряд
        public float damage { get; set; }
        public Action<Projectile> callback { get; set; }

        [Inject]
        private void Construct()
        {
            // Debug.LogWarning("Projectile construct");
        }

        private void FixedUpdate()
        {
            if (!_isMoving) return;
            MoveToTarget();
        }

        // Этот метод будет вызываться извне
        public void LaunchToTarget(Vector3 from, Vector3 to)
        {
            Assert.IsNotNull(callback, "ProjectilePool callback is not set");
            _targetPosition = to;
            transform.position = from;
            _isMoving = true;
        }

        private void MoveToTarget()
        {
            // Движение снаряда в направлении цели
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);

            transform.LookAt(_targetPosition);

            if (transform.position != _targetPosition) return;
            callback?.Invoke(this);
            _isMoving = false;
        }

        // private void OnHitTarget()
        // {
        //     isMoving = false; // Останавливаем движение
        //     Debug.Log("Target hit!");
        // }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;

            _isMoving = false;
            Debug.Log("Hit an enemy!");

            var enemy = other.GetComponent<EnemyHolder>();
            enemy.TakeDamage(damage);
            callback.Invoke(this);
            // Можно вызвать OnHitTarget для завершения движения
            // OnHitTarget();
        }

        public Action poolCallback { get; set; }
    }
}
