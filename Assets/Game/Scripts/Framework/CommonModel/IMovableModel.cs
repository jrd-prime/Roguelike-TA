using R3;
using UnityEngine;

namespace Game.Scripts.Framework.CommonModel
{
    public interface IMovableModel
    {
        public ReactiveProperty<Vector3> MoveDirection { get; }
        public ReactiveProperty<float> MoveSpeed { get; }
        public ReactiveProperty<float> RotationSpeed { get; }
        public ReactiveProperty<bool> IsMoving { get; }
        public void SetMoveDirection(Vector3 moveDirection);
    }
}
