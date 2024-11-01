using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Scripts.UI.MovementControl.FullScreen
{
    public class FullScreenMovementViewModel : IFullScreenMovementViewModel
    {
        public ReadOnlyReactiveProperty<bool> IsTouchPositionVisible => _model.IsTouchPositionVisible;
        public ReadOnlyReactiveProperty<Vector2> RingPosition => _model.RingPosition;

        private IFullScreenMovementModel _model;

        [Inject]
        private void Construct(IFullScreenMovementModel movementModel) => _model = movementModel;

        public void OnDownEvent(PointerDownEvent evt) => _model.OnDownEvent(evt);
        public void OnMoveEvent(PointerMoveEvent evt) => _model.OnMoveEvent(evt);
        public void OnUpEvent(PointerUpEvent evt) => _model.OnUpEvent(evt);
        public void OnOutEvent(PointerOutEvent evt) => _model.OnOutEvent(evt);
    }
}
