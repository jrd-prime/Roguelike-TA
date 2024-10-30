using R3;
using UnityEngine;

namespace Game.Scripts.UI.MovementControl.FullScreen
{
    public class FullScreenModel : IFullScreenModel
    {
        public ReactiveProperty<Vector3> MoveDirection { get; } = new(Vector3.zero);
        public ReactiveProperty<Vector3> ScreenOffset { get; } = new(Vector3.zero);

        public void SetMoveDirection(Vector3 value) => MoveDirection.Value = value;
    }
}
