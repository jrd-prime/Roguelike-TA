using UnityEngine;

namespace Game.Scripts.Framework.Animations
{
    public abstract class AnimatorBase
    {
        protected Animator Animator { get; }

        protected AnimatorBase(Animator animator)
        {
            Animator = animator;
        }

        public void SetAnimatorSpeed(float value) => Animator.speed = value;
    }
}
