using Game.Scripts.Framework.Configuration.SO;
using Game.Scripts.Framework.Managers.Settings;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.UI.MovementControl.FullScreen
{
    public class FullScreenMovementModel : IFullScreenMovementModel, IInitializable
    {
        public ReactiveProperty<Vector3> MoveDirection { get; } = new(Vector3.zero);
        public ReactiveProperty<bool> IsTouchPositionVisible { get; } = new(false);
        public ReactiveProperty<Vector2> RingPosition { get; } = new(Vector2.zero);

        private bool _isTouchActive;
        private float _offsetForFullSpeed = 50f;
        private Vector3 _moveInput;
        private Vector3 _startTouchPosition;
        private ISettingsManager _settingsManager;

        [Inject]
        private void Construct(ISettingsManager settingsManager) => _settingsManager = settingsManager;

        public void Initialize()
        {
            if (_settingsManager != null)
            {
                var movementControlSettings = _settingsManager.GetConfig<MovementControlSettings>();
                _offsetForFullSpeed = movementControlSettings.offsetForFullSpeed;
                return;
            }

            Debug.LogError("SettingsManager is null. Use default settings.");
        }

        private void SetMoveDirection(Vector3 value) => MoveDirection.Value = value;


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

            if (distance > _offsetForFullSpeed) offset = offset.normalized * _offsetForFullSpeed;

            _moveInput = offset / _offsetForFullSpeed;
            _moveInput = Vector2.ClampMagnitude(_moveInput, 1.0f);

            SetMoveDirection(new Vector3(_moveInput.x, 0, _moveInput.y * -1f));
        }

        public void OnUpEvent(PointerUpEvent _)
        {
            if (!_isTouchActive) return;
            ResetTouch();
        }

        public void OnOutEvent(PointerOutEvent _)
        {
            if (!_isTouchActive) return;
            ResetTouch();
        }

        private void ResetTouch()
        {
            _isTouchActive = false;
            _moveInput = Vector2.zero;

            SetMoveDirection(Vector3.zero);
            HideRing();
        }

        private void ShowRingAtTouchPosition(Vector3 position)
        {
            RingPosition.Value = new Vector2(position.x, position.y);

            ShowRing();
        }

        private void HideRing() => IsTouchPositionVisible.Value = false;
        private void ShowRing() => IsTouchPositionVisible.Value = true;
    }
}
