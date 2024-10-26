using Game.Scripts.Framework.Configuration.SO.Enemy;
using Game.Scripts.Framework.Managers.Enemy;
using Game.Scripts.Player.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyHolder : MonoBehaviour
    {
        [SerializeField] private EnemyHUD enemyHUD;
        private string EnemyID { get; set; }
        private float _attackDelay;
        private float _damage;
        private float _speed;
        private float _health;
        private float _currentHealth;
        private float _lastAttackTime;
        private EnemiesManager _enemiesManager;
        private bool _isAttacking;
        private IPlayerModel _playerModel;
        private Rigidbody _rb;
        private Animator _animator;
        private static readonly int IsReached = Animator.StringToHash("isReached");

        [Inject]
        private void Construct(EnemiesManager enemiesManager) => _enemiesManager = enemiesManager;

        private void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody>();
            Assert.IsNotNull(enemyHUD, $"HUDController is null. Add to {this}");
        }

        private void FixedUpdate()
        {
            _animator ??= GetComponentInChildren<Animator>();


            var enemyPosition = _rb.position;
            var targetPosition = _playerModel.Position.CurrentValue;

            var directionToTarget = (targetPosition - enemyPosition).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, lookRotation, 10 * Time.fixedDeltaTime);

            var distanceToTarget = Vector3.Distance(enemyPosition, targetPosition);

            if (distanceToTarget > 1f)
            {
                _rb.position = Vector3.MoveTowards(enemyPosition, targetPosition, _speed * 0.5f * Time.fixedDeltaTime);
                _animator.SetBool(IsReached, false);
                _isAttacking = false;
            }
            else
            {
                if (!_isAttacking && Time.time - _lastAttackTime >= _attackDelay)
                {
                    _animator.SetBool(IsReached, true);
                    _playerModel.TrackableAction.Invoke(_damage);
                    _lastAttackTime = Time.time;
                    _isAttacking = true;
                }
                else if (_isAttacking && Time.time - _lastAttackTime >= _attackDelay)
                {
                    _isAttacking = false;
                }

                if (Time.time - _lastAttackTime >= 0.1f)
                {
                    _animator.SetBool(IsReached, false);
                }
            }
        }


        public void FillEnemySettings(string enemyId, EnemySettings enemiesSettings, IPlayerModel trackableModelTarget)
        {
            EnemyID = enemyId;
            _playerModel = trackableModelTarget;
            _speed = enemiesSettings.speed;
            _attackDelay = enemiesSettings.attackDelay;
            _damage = enemiesSettings.damage;
            _health = enemiesSettings.health;
            _currentHealth = _health;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth > 0)
            {
                OnTakeDamage(damage);
                return;
            }

            OnDie();
        }

        private void OnDie() => _enemiesManager.EnemyDie(EnemyID);
        
        private void OnTakeDamage(float damage)
        {
            var hpPercent = _currentHealth / _health;
            enemyHUD.SetHp(hpPercent);
        }

        public void ClearEnemySettings()
        {
            EnemyID = default;
            _playerModel = default;
            _speed = default;
            _attackDelay = default;
            _damage = default;
            _health = default;
            _currentHealth = default;
            _lastAttackTime = default;
        }

        public void Spawn(string id, EnemySettings enemySettings, Vector3 spawnPoint,
            IPlayerModel followTargetModel)
        {
            FillEnemySettings(id, enemySettings, followTargetModel);
            transform.position = spawnPoint;
            gameObject.SetActive(true);
        }
    }
}
