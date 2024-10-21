using Game.Scripts.Framework.Sort.Player;
using R3;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Scripts.Framework.Sort.Camera
{
    public class CameraController : MonoBehaviour, ICameraController
    {
        [SerializeField] private UnityEngine.Camera mainCamera;

        private IPlayerViewModel _targetViewModel;
        private CompositeDisposable _subscribe = new();

        public UnityEngine.Camera MainCamera => mainCamera;

        public void SetFollowTarget(IPlayerViewModel target)
        {
            Debug.Log("SetFollowTarget " + target);

            transform.position = target.Position.CurrentValue;

            if (_targetViewModel != null) _subscribe?.Dispose();
            SubscribeToTargetPosition(target);
        }

        public void RemoveTarget()
        {
            Debug.Log("RemoveFollowTarget " + _targetViewModel);
            _targetViewModel = null;
            _subscribe?.Dispose();
        }

        public void MoveToPosition(Vector3 position)
        {
            // transform.DOMove(position, 1f).SetEase(Ease.OutQuad);
            transform.position = position;
        }

        private void SubscribeToTargetPosition(IPlayerViewModel target)
        {
            _targetViewModel = target;
            _targetViewModel.Position
                .Subscribe(MoveToPosition)
                .AddTo(_subscribe);
        }


        private void Awake()
        {
            Assert.IsNotNull(mainCamera, $"{nameof(mainCamera)} is null. {this}");
        }
    }
}
