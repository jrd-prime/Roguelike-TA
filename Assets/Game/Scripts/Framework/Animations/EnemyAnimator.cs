using Game.Scripts.Framework.Constants;
using UnityEngine;

namespace Game.Scripts.Framework.Animations
{
    public sealed class EnemyAnimator : AnimatorBase
    {
        public EnemyAnimator(Animator animator) : base(animator)
        {
        }

        public void SetToRunning()
        {
            Animator.SetBool(AnimConst.IsRunning, true);
            Animator.SetBool(AnimConst.IsAttacking, false);
        }

        public void SetToAttacking()
        {
            Animator.SetBool(AnimConst.IsRunning, false);
            Animator.SetBool(AnimConst.IsAttacking, true);
        }

        public void SetToIdle()
        {
            Animator.SetBool(AnimConst.IsAttacking, false);
            Animator.SetTrigger(AnimConst.Idle);
        }

        public void SetToDeath()
        {
            Animator.Play(AnimConst.Death, 0, 0f);
            Animator.SetTrigger(AnimConst.Death);
        }

    }
}
