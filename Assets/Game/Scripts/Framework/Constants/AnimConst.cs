using UnityEngine;

namespace Game.Scripts.Framework.Constants
{
    public static class AnimConst
    {
        // Player Animator Hashes
        public static readonly int MoveValue = Animator.StringToHash("MoveValue");
        public static readonly int IsMoving = Animator.StringToHash("IsMoving");
        public static readonly int IsInAction = Animator.StringToHash("IsInAction");

        // Enemy Animator Hashes
        public static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        public static readonly int Idle = Animator.StringToHash("Idle");
        public static readonly int IsRunning = Animator.StringToHash("IsRunning");
        public static readonly int Death = Animator.StringToHash("Death");

        // Enemy Animation Lengths
        public const int DeathAnimationLengthMs = 1833;
        public const int AttackAnimationLengthMs = 1000;
    }
}
