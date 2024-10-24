using System;
using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.Framework.Weapon
{
    [RequireComponent(typeof(SphereCollider))]
    public class Projectile : MonoBehaviour
    {
        public float speed = 100f; // Скорость движения снаряда
        public float hitThreshold = 0.1f; // Допустимое расстояние для попадания

        private Vector3 targetPoint; // Точка назначения
        private bool isMoving = false; // Флаг, показывающий, движется ли снаряд
        public float damage { get; set; }
        public Action callback { get; set; }

        private void FixedUpdate()
        {
            if (isMoving)
            {
                MoveToTarget();
            }

            if (!isMoving)
            {
                // Debug.LogWarning("Projectile is not moving");                
                callback?.Invoke();
            }
        }

        // Этот метод будет вызываться извне
        public void MoveToTarget(Vector3 startPoint, Vector3 targetPoint)
        {
            this.targetPoint = targetPoint; // Устанавливаем цель
            transform.position = startPoint; // Устанавливаем начальную точку
            isMoving = true; // Запускаем движение
        }

        private void MoveToTarget()
        {
            // Движение снаряда в направлении цели
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

            transform.LookAt(targetPoint);

            // Проверка попадания
            float distanceToTarget = Vector3.Distance(transform.position, targetPoint);
            if (distanceToTarget <= hitThreshold)
            {
                OnHitTarget();
            }
        }

        private void OnHitTarget()
        {
            isMoving = false; // Останавливаем движение
            Debug.Log("Target hit!");
        }

        void OnTriggerEnter(Collider other)
        {
            // Проверяем, что объект с тегом "Enemy"
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Hit an enemy!");

                var enemy = other.GetComponent<EnemyHolder>();
                enemy.TakeDamage(damage);

                callback.Invoke();
                // Можно вызвать OnHitTarget для завершения движения
                OnHitTarget();
            }
        }
    }
}
