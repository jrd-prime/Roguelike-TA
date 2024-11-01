using R3;
using UnityEngine;

namespace Game.Scripts.UI.MovementControl
{
    public interface IMovementControlModel
    {
        public ReactiveProperty<Vector3> MoveDirection { get; }
    }
}
