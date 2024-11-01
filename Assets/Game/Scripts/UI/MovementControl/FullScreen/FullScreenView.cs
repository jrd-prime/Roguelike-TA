using System;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Scripts.UI.MovementControl.FullScreen
{
    [RequireComponent(typeof(UIDocument))]
    public class FullScreenView : MonoBehaviour
    {
        private IFullScreenMovementViewModel _viewModel;

        private VisualElement _root;
        private VisualElement _ring;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IFullScreenMovementViewModel movementViewModel) => _viewModel = movementViewModel;

        private void Awake()
        {
            if (_viewModel == null) throw new NullReferenceException("ViewModel is null.");

            _root = GetComponent<UIDocument>().rootVisualElement;
            if (_root == null) throw new NullReferenceException("Root VisualElement is null.");

            _ring = _root.Q<VisualElement>(UIConst.FullScreenRingIDName);
            if (_ring == null)
                throw new NullReferenceException($"VisualElement with ID '{UIConst.FullScreenRingIDName}' not found.");

            _viewModel.IsTouchPositionVisible.Subscribe(IsTouchPositionVisible).AddTo(_disposables);
            _viewModel.RingPosition.Subscribe(SetRingPosition).AddTo(_disposables);

            _root.RegisterCallback<PointerDownEvent>(OnPointerDown);
            _root.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _root.RegisterCallback<PointerUpEvent>(OnPointerUp);
            _root.RegisterCallback<PointerOutEvent>(OnPointerCancel);
        }

        private void SetRingPosition(Vector2 position)
        {
            _ring.style.left = position.x;
            _ring.style.top = position.y;
        }

        private void IsTouchPositionVisible(bool value) =>
            _ring.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;


        private void OnPointerCancel(PointerOutEvent evt) => _viewModel.OnOutEvent(evt);
        private void OnPointerDown(PointerDownEvent evt) => _viewModel.OnDownEvent(evt);
        private void OnPointerMove(PointerMoveEvent evt) => _viewModel.OnMoveEvent(evt);
        private void OnPointerUp(PointerUpEvent evt) => _viewModel.OnUpEvent(evt);

        private void OnDestroy()
        {
            _disposables.Dispose();
            _root.UnregisterCallback<PointerDownEvent>(OnPointerDown);
            _root.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
            _root.UnregisterCallback<PointerUpEvent>(OnPointerUp);
            _root.UnregisterCallback<PointerOutEvent>(OnPointerCancel);
        }
    }
}
