using System.Collections;
using Game.Scripts.Framework.Constants;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyHolder : EnemyBase
    {
        private float _lastAttackTime;
        private bool _isAttacking;
        private float _fixedDT;

        private void FixedUpdate()
        {
            if (IsDead) return;

            _fixedDT = Time.fixedDeltaTime;
            TargetPosition = EnemySettingsDto.Target.Position.CurrentValue;
            RbPosition = Rb.position;

            RotateToTarget();

            if (CloseEnoughToAttack())
            {
                if (CanAttack()) Attack();
            }
            else
            {
                EnemyAnimator.SetToRunning();
                _isAttacking = false;
                MoveToTarget();
            }
        }


        private void Attack()
        {
            _isAttacking = true;
            _lastAttackTime = Time.time;
            EnemyAnimator.SetToAttacking();

            OnAttack();

            StartCoroutine(AttackAnimationDelay());
        }

        private IEnumerator AttackAnimationDelay()
        {
            yield return new WaitForSeconds(AnimConst.AttackAnimationLengthMs / 1000f);
            _isAttacking = false;
            EnemyAnimator.SetToIdle();
        }

        private void RotateToTarget()
        {
            var directionToTarget = (TargetPosition - Rb.position).normalized;
            var lookRotation = Quaternion.LookRotation(directionToTarget);
            Rb.rotation = Quaternion.Slerp(Rb.rotation, lookRotation, EnemySettingsDto.RotationSpeed * _fixedDT);
        }

        private void MoveToTarget()
        {
            Rb.position = Vector3.MoveTowards(RbPosition, TargetPosition, EnemySettingsDto.Speed * _fixedDT);
        }

        public void TakeDamage(float damage)
        {
            if (damage <= 0) return;

            CurrentHealth -= damage;

            if (CurrentHealth > 0)
                OnTakeDamage();
            else
                OnDie();
        }

        public override void OnDie()
        {
            if (IsDead) return;
            IsDead = true;
            Rb.isKinematic = true;
            EnemyAnimator.SetToDeath();
            EnemiesManager.EnemyDiedAsync(EnemySettingsDto.ID);
        }

        public override void OnTakeDamage()
        {
            var hpPercent = CurrentHealth / EnemySettingsDto.Health;
            enemyHUD.SetHp(hpPercent);
        }

        #region Conditions

        private bool CanAttack() => !_isAttacking && Time.time - _lastAttackTime > EnemySettingsDto.AttackDelayInSec;

        private bool CloseEnoughToAttack() =>
            Vector3.Distance(RbPosition, TargetPosition) <= EnemySettingsDto.AttackDistance;

        #endregion
    }
}
