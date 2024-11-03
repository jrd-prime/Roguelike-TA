using UnityEngine;

namespace Game.Scripts.Enemy
{
    public abstract class AnimatorBase
    {
        protected Animator Animator { get; }

        protected AnimatorBase(Animator animator) => Animator = animator;
    }
}
