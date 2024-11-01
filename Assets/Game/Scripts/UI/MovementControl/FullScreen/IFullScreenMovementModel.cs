using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.MovementControl.FullScreen
{
    public interface IFullScreenMovementModel : IMovementControlModel
    {
        public ReactiveProperty<bool> IsTouchPositionVisible { get; }
        public ReactiveProperty<Vector2> RingPosition { get; }

        public void OnDownEvent(PointerDownEvent evt);
        public void OnMoveEvent(PointerMoveEvent evt);
        public void OnUpEvent(PointerUpEvent evt);
        public void OnOutEvent(PointerOutEvent evt);
    }
}
