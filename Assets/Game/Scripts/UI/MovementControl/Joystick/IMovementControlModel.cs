using R3;
using UnityEngine;

public interface IMovementControlModel
{
    public ReactiveProperty<Vector3> MoveDirection { get; }
    public ReactiveProperty<Vector3> ScreenOffset { get; }
    public void SetMoveDirection(Vector3 value);
}
