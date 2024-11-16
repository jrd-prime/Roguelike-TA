using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.GamePlay.Components
{
    public sealed class Movement
    {
        private VisualElement _movementRoot;
        private VisualElement _ring;

        private readonly VisualElement _root;
        private readonly GameUIViewModel _viewModel;
        private readonly CompositeDisposable _disposables;

        public Movement(in GameUIViewModel viewModel, in VisualElement root, in CompositeDisposable disposables)
        {
            _viewModel = viewModel;
            _root = root;
            _disposables = disposables;
        }

        public void InitElements()
        {
            _movementRoot = _root.Q<VisualElement>(UIConst.MovementRootIDName);
            _ring = _root.Q<VisualElement>(UIConst.FullScreenRingIDName);
        }

        public void Init()
        {
            _movementRoot.RegisterCallback<PointerDownEvent>(OnPointerDown);
            _movementRoot.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _movementRoot.RegisterCallback<PointerUpEvent>(OnPointerUp);
            _movementRoot.RegisterCallback<PointerOutEvent>(OnPointerCancel);

            _viewModel.IsTouchPositionVisible.Subscribe(IsTouchPositionVisible).AddTo(_disposables);
            _viewModel.RingPosition.Subscribe(SetRingPosition).AddTo(_disposables);
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
    }
}
