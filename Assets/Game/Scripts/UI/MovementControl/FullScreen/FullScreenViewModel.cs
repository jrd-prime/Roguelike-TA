using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Scripts.UI.MovementControl.FullScreen
{
    public class FullScreenViewModel : IFullScreenViewModel
    {
        private IFullScreenModel _model;
        private VisualElement _ring;
        private Vector3 _startTouchPosition;
        private Vector3 _moveInput;
        private bool _isTouchActive;
        private readonly float _maxRadius = 100f;

        [Inject]
        private void Construct(IFullScreenModel model) => _model = model;

        public void SetRing(VisualElement ring)
        {
            _ring = ring;
            HideRing();
        }

        public void OnDownEvent(PointerDownEvent evt)
        {
            if (_isTouchActive) return;

            _isTouchActive = true;
            _startTouchPosition = evt.localPosition;
            ShowRingAtTouchPosition(_startTouchPosition);
        }

        public void OnMoveEvent(PointerMoveEvent evt)
        {
            if (!_isTouchActive) return;

            var currentPosition = evt.localPosition;
            var offset = currentPosition - _startTouchPosition;

            var distance = offset.magnitude;
            if (distance > _maxRadius)
                offset = offset.normalized * _maxRadius;

            _moveInput = offset / _maxRadius;
            _moveInput = Vector2.ClampMagnitude(_moveInput, 1.0f);
            _model.SetMoveDirection(new Vector3(_moveInput.x, 0, _moveInput.y * -1f));
        }

        public void OnUpEvent(PointerUpEvent evt)
        {
            if (!_isTouchActive) return;
            ResetTouch();
        }

        public void OnOutEvent(PointerOutEvent evt)
        {
            if (!_isTouchActive) return;
            ResetTouch();
        }

        private void ResetTouch()
        {
            _isTouchActive = false;
            _moveInput = Vector2.zero;
            _model.SetMoveDirection(Vector3.zero);
            HideRing();
        }

        private void ShowRingAtTouchPosition(Vector3 position)
        {
            _ring.style.display = DisplayStyle.Flex;
            _ring.style.left = position.x - 10;
            _ring.style.top = position.y - 10;
        }

        private void HideRing() => _ring.style.display = DisplayStyle.None;
    }
}
