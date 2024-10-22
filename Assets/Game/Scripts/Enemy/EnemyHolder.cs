using Game.Scripts.Framework.ScriptableObjects;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyHolder : MonoBehaviour
    {
        private PlayerModel _target;
        private Rigidbody _rb;
        public string EnemyID { get; private set; }
        private string _enemyName;
        private float _attackDelay;
        private float _damage;
        private float _speed;
        private float _lastAttackTime = 0f;

        private void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var enemyPosition = _rb.position;
            var targetPosition = _target.Position.CurrentValue;
            var distanceToTarget = Vector3.Distance(enemyPosition, targetPosition);
            // Вычисляем направление к цели
            Vector3 directionToTarget = (targetPosition - enemyPosition).normalized;

            // Вычисляем нужное вращение
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

            // Применяем плавное вращение
            _rb.rotation = Quaternion.Slerp(_rb.rotation, lookRotation, 180 * Time.fixedDeltaTime);


            if (distanceToTarget > 2f)
            {
                _rb.position =
                    Vector3.MoveTowards(enemyPosition, targetPosition, _speed * Time.fixedDeltaTime);
            }
            else
            {
                if (Time.time - _lastAttackTime >= _attackDelay)
                {
                    _target.TakeDamage(_damage, _enemyName);
                    _lastAttackTime = Time.time;
                }
            }
        }

        public void FillEnemySettings(string enemyId, EnemySettings enemiesSettings, PlayerModel targetModel)
        {
            EnemyID = enemyId;
            _enemyName = enemiesSettings.enemyName;
            _target = targetModel;
            _speed = enemiesSettings.speed;
            _attackDelay = enemiesSettings.attackDelay;
            _damage = enemiesSettings.damage;
        }

        public void OnSpawn()
        {
            // Debug.Log("Enemy spawned");
        }

        public void OnDespawn()
        {
            // Debug.Log("Enemy despawned");
        }
    }
}
