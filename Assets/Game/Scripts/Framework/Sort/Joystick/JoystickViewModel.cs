using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Sort.Joystick
{
    public class JoystickViewModel : IInitializable
    {
        private JoystickModel _model;
        private Vector3 _moveInput;

        private Vector3 screenOffset => _model.ScreenOffset.Value;
        private VisualElement joystickHandle => _model.JoystickHandle;
        private VisualElement joystickRing => _model.JoystickRing;

        [Inject]
        private void Construct(JoystickModel model)
        {
            _model = model;
        }

        public void Initialize()
        {
        }

        public void OnOutEvent(PointerOutEvent evt)
        {
            _model.MoveDirection.Value = Vector3.zero;
            ResetJoystickPosition();
        }

        public void OnDownEvent(PointerDownEvent evt) => UpdateJoystickPosition(evt.position);

        public void OnMoveEvent(PointerMoveEvent evt) => UpdateJoystickPosition(evt.position);


        private void UpdateJoystickPosition(Vector3 position)
        {
            var po = position - screenOffset;

            // Debug.LogWarning($"pos: {po}");
            joystickHandle.transform.position = po;

            _moveInput = po / (joystickRing.layout.width / 2);
            _moveInput = Vector2.ClampMagnitude(_moveInput, 1.0f);

            _model.MoveDirection.Value = new Vector3(_moveInput.x, 0, _moveInput.y * -1f);
        }

        public void OnUpEvent(PointerUpEvent evt) => ResetJoystickPosition();


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
