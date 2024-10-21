using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace BackwoodsLife.Scripts.Framework.Manager.Input
{
    public struct TouchData
    {
        public int LayerId;
        public Vector2 TouchPositionOnScreen;
    }

    public sealed class MobileInput : MonoBehaviour, IInput
    {
        // public event Action onLMousePress;
        // public event Action<Vector2> onLMouseDrag;
        // public event Action onLMouseRelease;
        // public event Action onLMouseTaped;
        // public event Action<Vector3> OnSingleClick;

        public ReactiveProperty<Vector2> TouchV2 { get; } = new();

        public ReactiveProperty<TouchData> TouchWithData { get; } = new ReactiveProperty<TouchData>();

        private JInputActions _gameInputActions;
        private EventSystem _event;

        public void ChangeActionMap(string actionMapName)
        {
            throw new NotImplementedException();
        }

        [Inject]
        private void Construct(EventSystem eventSystem)
        {
            _event = eventSystem;
        }

        private void Awake()
        {
            _gameInputActions = new JInputActions();
            _gameInputActions.Enable();

            // _gameInputActions.UI.TouchPosition.performed += OnTouchData;
            // _gameInputActions.UI.Click.performed += OnTouchData;
        }

        private bool IsPointerOverUI(Vector2 position)
        {
            var pointer = new PointerEventData(EventSystem.current);
            pointer.position = position;
            var raycastResults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointer, raycastResults);

            return raycastResults.Count > 0 && raycastResults.Any(result => result is { distance: 0, isValid: true });
        }

        // private void OnTouchData(InputAction.CallbackContext obj)
        // {
        //     var position = obj.ReadValue<Vector2>();
        //
        //     // Debug.LogWarning(position);
        //     if (IsPointerOverUI(position)) return;
        //
        //
        //     TouchV2.Value = position;
        //
        //     // TODO inject camera
        //     Ray ray = UnityEngine.Camera.main.ScreenPointToRay(position);
        //     RaycastHit hit;
        //
        //     // Perform the raycast
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         // UnityEngine.Debug.Log("Touched object: " + hit.collider.name);
        //         // UnityEngine.Debug.LogWarning("hti point " + hit.point);
        //         // UnityEngine.Debug.LogWarning("collider layer = " + hit.collider.gameObject.layer);
        //
        //         var layer = hit.collider.gameObject.layer;
        //
        //         if (position == Vector2.zero) return;
        //
        //         TouchWithData.Value = new TouchData() { LayerId = layer, TouchPositionOnScreen = position };
        //
        //         // Implement your logic for interacting with the touched object
        //         HandleTouch(hit);
        //     }
        // }

        private void HandleTouch(RaycastHit hit)
        {
            // Example logic: Change color of the touched object
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            // if (renderer != null)
            // {
            //     renderer.material.color = Color.gray;
            // }
        }
    }
}
