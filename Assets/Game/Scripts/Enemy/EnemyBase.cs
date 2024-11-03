using System;
using Game.Scripts.Dto;
using Game.Scripts.Framework.Managers.Enemy;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] public EnemyHUD enemyHUD;
        public EnemySettingsDto EnemySettingsDto { get; private set; }

        protected Vector3 TargetPosition = Vector3.zero;
        protected Vector3 RbPosition = Vector3.zero;
        protected EnemyAnimator EnemyAnimator;
        protected bool IsDead = false;
        protected IEnemiesManager EnemiesManager;
        protected float CurrentHealth;
        protected Rigidbody Rb;


        private bool _isInitialized;

        [Inject]
        private void Construct(IEnemiesManager enemiesManager) => EnemiesManager = enemiesManager;

        protected void Start()
        {
            if (!_isInitialized) throw new Exception("Enemy is not initialized!");
            if (EnemiesManager == null) throw new NullReferenceException("EnemiesManager is null");
            if (enemyHUD == null) throw new NullReferenceException("HUDController is null. Add to " + this);

            EnemyAnimator = new EnemyAnimator(EnemySettingsDto.Animator);

            Rb = gameObject.GetComponent<Rigidbody>();
        }

        public void Initialize(EnemySettingsDto settings)
        {
            EnemySettingsDto = settings;
            CurrentHealth = settings.Health;
            _isInitialized = true;
        }

        protected void OnAttack() => EnemySettingsDto.Target.TrackableAction?.Invoke(EnemySettingsDto.Damage);


        public void ResetEnemy()
        {
            _isInitialized = false;
            CurrentHealth = 0f;
            enemyHUD.ResetHUD();
            Rb.isKinematic = false;
        }

        public abstract void OnTakeDamage();
        public abstract void OnDie();
    }
}
