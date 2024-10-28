using UnityEngine;

namespace Game.Scripts.Framework.Constants
{
    public static class AnimConst
    {
        public static readonly int IsTargetReached = Animator.StringToHash("isReached");
        public static readonly int MoveValue = Animator.StringToHash("MoveValue");
        public static readonly int IsMoving = Animator.StringToHash("IsMoving");
        public static readonly int IsInAction = Animator.StringToHash("IsInAction");
    }
}
