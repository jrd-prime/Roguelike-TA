using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Scripts.Framework.Sort.Joystick
{
    [RequireComponent(typeof(UIDocument))]
    public class JoystickView : MonoBehaviour
    {
        [SerializeField] private float screenOffsetX = 600f;
        [SerializeField] private float screenOffsetY = 190f;
        [SerializeField] private float centerX = 75f;
        [SerializeField] private float centerY = 75f;

        private JoystickViewModel _viewModel;
        private VisualElement _root;

        [Inject]
        private void Construct(JoystickViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnEnable()
        {
            Assert.IsNotNull(_viewModel,
                $"ViewModel is null. Ensure that \"{this}\" is added to auto-injection in GameSceneContext prefab");

            _root = GetComponent<UIDocument>().rootVisualElement;
            var joystickHandle = _root.Q<VisualElement>(UIName.JoystickHandle);
            var joystickRing = _root.Q<VisualElement>(UIName.JoystickRing);

            _viewModel.SetScreenOffset(new Vector3(screenOffsetX, screenOffsetY, 0) + new Vector3(centerX, centerY, 0));
            _viewModel.SetJoystickVisual(joystickHandle, joystickRing);

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
