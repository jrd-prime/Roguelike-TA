using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Scripts.UI.MovementControl.FullScreen
{
    [RequireComponent(typeof(UIDocument))]
    public class FullScreenView : MonoBehaviour
    {
        private IFullScreenViewModel _viewModel;
        private VisualElement _root;

        [Inject]
        private void Construct(IFullScreenViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            Assert.IsNotNull(_viewModel,
                $"ViewModel is null. Ensure that \"{this}\" is added to auto-injection in GameScope prefab");

            _root = GetComponent<UIDocument>().rootVisualElement;

            var ring = _root.Q<VisualElement>("ring");
            _viewModel.SetRing(ring);

            // Регистрируем обработчики событий для всего экрана
            _root.RegisterCallback<PointerDownEvent>(OnPointerDown);
            _root.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _root.RegisterCallback<PointerUpEvent>(OnPointerUp);
            _root.RegisterCallback<PointerOutEvent>(OnPointerCancel);
        }

        private void OnPointerCancel(PointerOutEvent evt) => _viewModel.OnOutEvent(evt);
        private void OnPointerDown(PointerDownEvent evt) => _viewModel.OnDownEvent(evt);
        private void OnPointerMove(PointerMoveEvent evt) => _viewModel.OnMoveEvent(evt);
        private void OnPointerUp(PointerUpEvent evt) => _viewModel.OnUpEvent(evt);

        private void OnDestroy()
        {
            _root.UnregisterCallback<PointerDownEvent>(OnPointerDown);
            _root.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
            _root.UnregisterCallback<PointerUpEvent>(OnPointerUp);
            _root.UnregisterCallback<PointerOutEvent>(OnPointerCancel);
        }
    }
}
