using UnityEngine;

namespace Game.Scripts.Framework.Sort.Animations
{
    public static class AnimConst
    {
        public static readonly int MoveValue = Animator.StringToHash("MoveValue");
        public static readonly int IsMoving = Animator.StringToHash("IsMoving");
        public static readonly int IsGathering = Animator.StringToHash("IsGathering");
        public static readonly int IsInAction = Animator.StringToHash("IsInAction");
        public static readonly int IsCutting = Animator.StringToHash("IsCutting");
        public static readonly int IsMining = Animator.StringToHash("IsMining");
    }
}
