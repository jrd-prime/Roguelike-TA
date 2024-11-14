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
    }
}
