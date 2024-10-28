using Game.Scripts.Framework.Constants;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyHolder : EnemyBase
    {
        private void FixedUpdate()
        {
            if (!IsInitialized) return;

            TargetPosition = Target.Position.CurrentValue;
            RbPosition = Rb.position;

            if (!CloseEnoughToAttack()) MoveToTarget();
            else PerformAttack();

            RotateToTarget();
        }

        private void RotateToTarget()
        {
            var directionToTarget = (TargetPosition - Rb.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            Rb.rotation = Quaternion.Slerp(Rb.rotation, lookRotation, RotationSpeed * Time.fixedDeltaTime);
        }

        private void MoveToTarget()
        {
            Rb.position =
                Vector3.MoveTowards(RbPosition, TargetPosition, Settings.Speed * Time.fixedDeltaTime);

            Animator.SetBool(AnimConst.IsTargetReached, false);
            IsAttacking = false;
        }

        private void PerformAttack()
        {
            if (CanStartAttack())
            {
                Animator.SetBool(AnimConst.IsTargetReached, true);
                Attack();
                LastAttackTime = Time.time;
                IsAttacking = true;
            }
            else if (CanStopAttack()) IsAttacking = false;

            // skip attack animation // TODO refactor anim blend
            if (Time.time - LastAttackTime >= 0.1f) Animator.SetBool(AnimConst.IsTargetReached, false);
        }

        private bool CanStopAttack() => IsAttacking && Time.time - LastAttackTime >= Settings.AttackDelayMs;

        private bool CanStartAttack() => !IsAttacking && Time.time - LastAttackTime >= Settings.AttackDelayMs;

        private bool CloseEnoughToAttack() => Vector3.Distance(RbPosition, TargetPosition) < DistanceToAttack;


        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth > 0)
            {
                OnTakeDamage();
                return;
            }

            OnDie();
        }

        public override void OnDie() => EnemiesManager.EnemyDied(Settings.ID);

        public override void OnTakeDamage()
        {
            var hpPercent = CurrentHealth / Settings.Health;
            enemyHUD.SetHp(hpPercent);
        }
    }
}
