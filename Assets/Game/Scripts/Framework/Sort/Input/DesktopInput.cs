using System;
using R3;
using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Manager.Input
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        public event Action<Vector2> onLMouseDrag;
        public event Action onLMousePress;
        public event Action onLMouseRelease;
        public event Action onLMouseTaped;
        public event Action<Vector3> OnSingleClick;

        private JInputActions _gameInputActions;

        private void Awake()
        {
            // _gameInputActions = new JInputActions();
            // _gameInputActions.Enable();

            Debug.LogWarning("OnEnable");
            // _gameInputActions.UI.SingleClick.performed += OnSinClick;
        }

        private void OnEnable()
        {
            // _gameInputActions.Gameplay.LMBHolded.performed += OnLMBHolded;
            // _gameInputActions.Gameplay.LMBHolded.canceled += OnLMBReleased;
            // _gameInputActions.Gameplay.LMBTaped.performed += OnLMBTaped;
        }

        // private void OnSinClick(CallbackContext obj)
        // {
        //     Debug.LogWarning("OnSinClick");
        //
        //     Debug.LogWarning(this + " " + obj);
        //
        //     OnSingleClick?.Invoke(obj.ReadValue<Vector3>());
        // }


        // private void OnMenu(CallbackContext obj)
        // {
        //     Debug.LogWarning("OnMenu");
        // }

        private void OnDisable()
        {
            _gameInputActions.Disable();
            // _gameInputActions.UI.SingleClick.performed -= OnSinClick;

            // _gameInputActions.Gameplay.LMBHolded.performed -= OnLMBHolded;
            // _gameInputActions.Gameplay.LMBHolded.canceled -= OnLMBReleased;
            // _gameInputActions.Gameplay.LMBTaped.performed -= OnLMBTaped;
        }

        private bool _isMouseHolded;

        private void Update()
        {
            if (_isMouseHolded)
            {
                CheckHoldedMouseMovementDirection(GetHoldedMouseMovementDirection());
            }
        }

        // public void OnLMBHolded(CallbackContext ctx)
        // {
        //     onLMousePress?.Invoke();
        //     _isMouseHolded = true;
        // }

        // private void OnLMBReleased(CallbackContext ctx)
        // {
        //     onLMouseRelease?.Invoke();
        //     _isPreviousMousePositionInitialized = false;
        //     _isMouseHolded = false;
        // }

        // private void OnLMBTaped(CallbackContext ctx)
        // {
        //     onLMouseTaped?.Invoke();
        // }

        private Vector3 _previousFrameMousePosition;
        private bool _isPreviousMousePositionInitialized;

        private Vector2 GetHoldedMouseMovementDirection()
        {
            if (!_isPreviousMousePositionInitialized)
            {
                _previousFrameMousePosition = UnityEngine.Input.mousePosition;
                _isPreviousMousePositionInitialized = true;
                return new Vector2(0, 0);
            }
            else
            {
                Vector2 dirVect = new Vector2(_previousFrameMousePosition.x - UnityEngine.Input.mousePosition.x,
                    _previousFrameMousePosition.y - UnityEngine.Input.mousePosition.y);
                _previousFrameMousePosition = UnityEngine.Input.mousePosition;
                return dirVect;
            }
        }

        private void CheckHoldedMouseMovementDirection(Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                onLMouseDrag?.Invoke(direction);
            }
        }

        private string _currentActionMapName;

        public ReactiveProperty<TouchData> TouchWithData { get; }

        public void ChangeActionMap(string actionMapName)
        {
            _gameInputActions.asset.FindActionMap(_currentActionMapName).Disable();
            _gameInputActions.asset.FindActionMap(actionMapName).Enable();
            _currentActionMapName = _gameInputActions.asset.FindActionMap(actionMapName).name;
        }
    }
}
