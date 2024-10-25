using Game.Scripts.Framework.ScriptableObjects.Enemy;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using VContainer;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyHolder : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private Rigidbody _rb;
        public string EnemyID { get; private set; }
        private string _enemyName;
        private float _attackDelay;
        private float _damage;
        private float _speed;
        private float _health;
        private float _currentHealth;
        private float _lastAttackTime = 0f;
        private EnemiesManager _enemiesManager;

        [FormerlySerializedAs("enemyHUDController")] [FormerlySerializedAs("_hudController")] [SerializeField]
        private EnemyHUD enemyHUD;

        [Inject]
        private void Construct(EnemiesManager enemiesManager)
        {
            _enemiesManager = enemiesManager;
        }

        private void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody>();
            Assert.IsNotNull(enemyHUD, $"HUDController is null. Add to {this}");
        }

        private void FixedUpdate()
        {
            var enemyPosition = _rb.position;
            var targetPosition = _playerModel.Position.CurrentValue;
            var distanceToTarget = Vector3.Distance(enemyPosition, targetPosition);
            // Вычисляем направление к цели
            Vector3 directionToTarget = (targetPosition - enemyPosition).normalized;

            // Вычисляем нужное вращение
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

            // Применяем плавное вращение
            _rb.rotation = Quaternion.Slerp(_rb.rotation, lookRotation, 180 * Time.fixedDeltaTime);


            if (distanceToTarget > 1f)
            {
                _rb.position =
                    Vector3.MoveTowards(enemyPosition, targetPosition, _speed * 0.5f * Time.fixedDeltaTime);
            }
            else
            {
                if (Time.time - _lastAttackTime >= _attackDelay)
                {
                    _playerModel.TakeDamage(_damage, _enemyName);
                    _lastAttackTime = Time.time;
                }
            }
        }

        public void FillEnemySettings(string enemyId, EnemySettings enemiesSettings, PlayerModel targetModel)
        {
            EnemyID = enemyId;
            _enemyName = enemiesSettings.enemyName;
            _playerModel = targetModel;
            _speed = enemiesSettings.speed;
            _attackDelay = enemiesSettings.attackDelay;
            _damage = enemiesSettings.damage;
            _health = enemiesSettings.health;
            _currentHealth = _health;
        }

        public void TakeDamage(float damage)
        {
            Debug.LogWarning($"took {damage} damage. {_currentHealth} / {_health}");

            _currentHealth -= damage;

            if (_currentHealth > 0)
            {
                OnTakeDamage(damage);
                return;
            }

            OnDie();
        }

        private void OnDie()
        {
            Debug.LogWarning("On die = " + EnemyID);
            _enemiesManager.EnemyDie(EnemyID);
        }

        public void OnSpawn()
        {
            // Debug.Log("Enemy spawned");
        }

        public void OnDespawn()
        {
            // Debug.Log("Enemy despawned");
        }

        public void OnTakeDamage(float damage)
        {
            Debug.LogWarning($"I'm taking damage! Was: {_currentHealth + damage} Now: {_currentHealth}");

            var hpPercent = _currentHealth / _health;

            Debug.LogWarning($"hpPercent = {hpPercent}");

            enemyHUD.SetHp(hpPercent);
        }

        public void ClearEnemySettings()
        {
            EnemyID = default;
            _enemyName = default;
            _playerModel = default;
            _speed = default;
            _attackDelay = default;
            _damage = default;
            _health = default;
            _currentHealth = default;
        }
    }
}
