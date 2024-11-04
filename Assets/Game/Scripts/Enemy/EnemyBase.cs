using System;
using Game.Scripts.Dto;
using Game.Scripts.Framework.Animations;
using Game.Scripts.Framework.Managers.Enemy;
using Game.Scripts.UI.PopUpText;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] public EnemyHUD enemyHUD;

        public EnemySettingsDto SettingsDto { get; private set; }
        protected PopUpTextManager PopUpTextManager { get; private set; }

        protected Vector3 TargetPosition = Vector3.zero;
        protected Vector3 RbPosition = Vector3.zero;
        protected EnemyAnimator EnemyAnimator;
        protected IEnemiesManager EnemiesManager;
        protected Rigidbody Rb;
        protected float CurrentHealth;
        protected bool IsDead;

        private bool _isInitialized;

        [Inject]
        private void Construct(IEnemiesManager enemiesManager, PopUpTextManager popUpTextManager)
        {
            EnemiesManager = enemiesManager;
            PopUpTextManager = popUpTextManager;
        }


        protected void Start()
        {
            if (!_isInitialized) throw new Exception("Enemy is not initialized!");
            if (EnemiesManager == null) throw new NullReferenceException("EnemiesManager is null");
            if (enemyHUD == null) throw new NullReferenceException("HUDController is null. Add to " + this);

            Rb = gameObject.GetComponent<Rigidbody>();
        }

        public void Initialize(EnemySettingsDto settings)
        {
            EnemyAnimator = new EnemyAnimator(settings.Animator);

            SettingsDto = settings;
            CurrentHealth = settings.Health;
            _isInitialized = true;
        }

        protected void OnAttack() => SettingsDto.Target.TrackableAction?.Invoke(SettingsDto.Damage);

        public void ResetEnemy()
        {
            _isInitialized = false;
            EnemyAnimator = null;
            CurrentHealth = 0f;
            enemyHUD.ResetHUD();
            Rb.isKinematic = false;
            var skin = GetComponentInChildren<EnemySkin>();
            Destroy(skin.gameObject);
            IsDead = false;
        }

        protected abstract void OnTakeDamage(float damage);
        protected abstract void OnDie();
    }
}
