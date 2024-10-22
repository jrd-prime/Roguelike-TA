using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Scripts.UI.Joystick
{
    public class JoystickViewModel
    {
        private JoystickModel _model;
        private Vector3 _moveInput;

        private Vector3 screenOffset => _model.ScreenOffset.Value;
        private VisualElement joystickHandle => _model.JoystickHandle;
        private VisualElement joystickRing => _model.JoystickRing;

        [Inject]
        private void Construct(JoystickModel model) => _model = model;


        public void OnDownEvent(PointerDownEvent evt) => UpdateJoystickPosition(evt.position);

        public void OnMoveEvent(PointerMoveEvent evt) => UpdateJoystickPosition(evt.position);

        public void OnUpEvent(PointerUpEvent evt) => ResetJoystickPosition();

        public void OnOutEvent(PointerOutEvent _)
        {
            _model.MoveDirection.Value = Vector3.zero;
            ResetJoystickPosition();
        }


        private void UpdateJoystickPosition(Vector3 position)
        {
            var handlePosition = position - screenOffset;

            joystickHandle.transform.position = handlePosition;

            _moveInput = handlePosition / (joystickRing.layout.width / 2);
            _moveInput = Vector2.ClampMagnitude(_moveInput, 1.0f);

            _model.MoveDirection.Value = new Vector3(_moveInput.x, 0, _moveInput.y * -1f);
        }

        private void ResetJoystickPosition()
        {
            _moveInput = Vector2.zero;
            joystickHandle.transform.position = Vector2.zero;
        }

        public void SetScreenOffset(Vector3 offset) => _model.ScreenOffset.Value = offset;

        public void SetJoystickVisual(VisualElement handle, VisualElement ring)
        {
            _model.SetJoystickHandle(handle);
            _model.SetJoystickRing(ring);
        }
    }
}
