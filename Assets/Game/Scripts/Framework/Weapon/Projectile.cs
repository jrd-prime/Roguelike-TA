using System;
using UnityEngine;

namespace Game.Scripts.Framework.Weapon
{
    [RequireComponent(
        // typeof(Rigidbody), 
        typeof(SphereCollider))]
    public class Projectile : MonoBehaviour
    {
        public float speed = 100f; // Скорость движения снаряда
        public float hitThreshold = 0.1f; // Допустимое расстояние для попадания

        private Vector3 targetPoint; // Точка назначения
        private bool isMoving = false; // Флаг, показывающий, движется ли снаряд
        private Rigidbody _rb;
        private Vector3 _st;
        private Vector3 _end;

        private void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (isMoving)
            {
                MoveToTarget();
            }
        }

        private void OnDrawGizmos()
        {
            // Установите цвет линии
            Gizmos.color = Color.red;

            // Нарисуйте линию от старта к цели
            Gizmos.DrawLine(_st, _end);
        }

        // Этот метод будет вызываться извне
        public void MoveToTarget(Vector3 startPoint, Vector3 targetPoint)
        {
            _st = startPoint;
            _end = targetPoint;

            Debug.LogWarning("Move to target");
            this.targetPoint = targetPoint; // Устанавливаем цель
            transform.position = startPoint; // Устанавливаем начальную точку
            isMoving = true; // Запускаем движение
        }

        private void MoveToTarget()
        {
            Debug.LogWarning($"position when start shoot = {targetPoint}");
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

            // Логика при попадании
            Destroy(gameObject); // Уничтожить снаряд после попадания (опционально)
        }

        void OnTriggerEnter(Collider other)
        {
            // Проверяем, что объект с тегом "Enemy"
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Hit an enemy!");

                other.gameObject.SetActive(false);
                // Можно вызвать OnHitTarget для завершения движения
                OnHitTarget();
            }
        }
    }
}
