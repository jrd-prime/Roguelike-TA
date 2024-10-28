using System;
using Game.Scripts.Framework.CommonModel;
using Game.Scripts.Framework.Managers.Enemy;
using Game.Scripts.Player.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : MonoBehaviour, IEnemy
    {
        [SerializeField] protected EnemyHUD enemyHUD;

        protected Rigidbody _rb;
        protected EnemySettingsDto _settings;
        protected Vector3 _targetCurrentPosition = Vector3.zero;
        protected readonly CompositeDisposable _disposables = new();

        protected float _attackDelay;
        protected float _damage;
        protected float _speed;
        protected float _health;

        protected float _currentHealth;
        protected bool isInitialized;
        protected float _lastAttackTime;
        protected bool _isAttacking;

        protected EnemiesManager _enemiesManager;
        protected IPlayerModel _playerModel;
        protected Animator _animator;

        private void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody>();
            Assert.IsNotNull(enemyHUD, $"HUDController is null. Add to {this}");
            Debug.LogWarning("awake enemy");
        }


        protected void Start()
        {
            if (!isInitialized) throw new Exception("Enemy is not initialized!");

            Subscribe();
        }


        protected void OnDisable() => Unsubscribe();

        protected void OnDestroy() => _disposables.Dispose();


        public abstract void Attack(ITrackableModel target);
        public abstract void OnTakeDamage();
        public abstract void OnDie(Action callback);
        protected abstract void Subscribe();
        protected abstract void Unsubscribe();


        public virtual void Initialize(EnemySettingsDto settings)
        {
            _settings = settings;
            _currentHealth = settings.Health;
            _animator = settings.Animator;
            _lastAttackTime = 0f;
            isInitialized = true;
        }

        public void ResetEnemy()
        {
            isInitialized = false;
            _currentHealth = 0f;
            _lastAttackTime = 0f;
        }
    }

    public class EnemyHolder : EnemyBase
    {
        public string EnemyID { get; private set; }


        private static readonly int IsReached = Animator.StringToHash("isReached");

        [Inject]
        private void Construct(EnemiesManager enemiesManager) => _enemiesManager = enemiesManager;


        protected override void Subscribe()
        {
            _settings.Target.Position
                .Subscribe(position => _targetCurrentPosition = position)
                .AddTo(_disposables);
        }

        protected override void Unsubscribe()
        {
        }


        private void FixedUpdate()
        {
            if (!isInitialized) return;

            if (expr)
            {
                
            }


            var rbPosition = _rb.position;
            var targetPosition = _settings.Target.Position.CurrentValue;

            var directionToTarget = (targetPosition - rbPosition).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, lookRotation, 10 * Time.fixedDeltaTime);

            var distanceToTarget = Vector3.Distance(rbPosition, targetPosition);

            if (distanceToTarget > 1f)
            {
                _rb.position = Vector3.MoveTowards(rbPosition, targetPosition, _speed * 0.5f * Time.fixedDeltaTime);
                _animator.SetBool(IsReached, false);
                _isAttacking = false;
            }
            else
            {
                if (!_isAttacking && Time.time - _lastAttackTime >= _attackDelay)
                {
                    _animator.SetBool(IsReached, true);
                    _settings.Target.TrackableAction.Invoke(_settings.Damage);
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

        public override void Attack(ITrackableModel target)
        {
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth > 0)
            {
                OnTakeDamage();
                return;
            }

            OnDie(null);
        }

        public override void OnDie(Action callback) => _enemiesManager.EnemyDie(_settings.ID);

        public override void OnTakeDamage()
        {
            var hpPercent = _currentHealth / _settings.Health;
            enemyHUD.SetHp(hpPercent);
        }
    }

    public interface IEnemy
    {
        public void Attack(ITrackableModel target);
        public void OnTakeDamage();
        public void OnDie(Action callback);
    }
}
