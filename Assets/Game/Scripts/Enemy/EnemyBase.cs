using System;
using Game.Scripts.Dto;
using Game.Scripts.Framework.CommonModel;
using Game.Scripts.Framework.Managers.Enemy;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] protected EnemyHUD enemyHUD;

        protected Rigidbody Rb;
        public EnemySettingsDto Settings { get; private set; }
        protected Vector3 TargetPosition = Vector3.zero;
        protected Vector3 RbPosition = Vector3.zero;

        // TODO move to settings
        protected const float DistanceToAttack = 1f;
        protected const float RotationSpeed = 10f;

        protected IEnemiesManager EnemiesManager;

        protected float CurrentHealth { get; set; }
        protected bool IsInitialized;
        protected float LastAttackTime;
        protected bool IsAttacking;

        public string ID => Settings.ID;
        protected Animator Animator => Settings.Animator;
        protected ITrackableModel Target => Settings.Target;

        [Inject]
        private void Construct(IEnemiesManager enemiesManager) => EnemiesManager = enemiesManager;

        protected void Start()
        {
            if (!IsInitialized) throw new Exception("Enemy is not initialized!");

            Rb = gameObject.GetComponent<Rigidbody>();
            Assert.IsNotNull(EnemiesManager, "EnemiesManager is null");
            Assert.IsNotNull(enemyHUD, $"HUDController is null. Add to {this}");
        }

        public void Initialize(EnemySettingsDto settings)
        {
            Settings = settings;
            CurrentHealth = settings.Health;
            LastAttackTime = 0f;
            IsInitialized = true;
        }

        protected void Attack() => Target.TrackableAction?.Invoke(Settings.Damage);


        public void ResetEnemy()
        {
            IsInitialized = false;
            IsAttacking = false;
            CurrentHealth = 0f;
            LastAttackTime = 0f;
            enemyHUD.ResetHUD();
        }

        public abstract void OnTakeDamage();
        public abstract void OnDie();
    }
}
