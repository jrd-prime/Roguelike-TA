using Game.Scripts.Framework.Systems.Follow;
using R3;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Scripts.Framework.Camera
{
    public class CameraController : MonoBehaviour, ICameraController
    {
        [SerializeField] private UnityEngine.Camera mainCamera;

        private ITrackable _targetViewModel;
        private readonly CompositeDisposable _disposables = new();

        private void Awake()
        {
            Assert.IsNotNull(mainCamera, $"MainCamera is null. {this}");
        }

        public void SetFollowTarget(ITrackable target)
        {
            transform.position = target.Position.CurrentValue;

            if (_targetViewModel != null) _disposables?.Dispose();
            SubscribeToTargetPosition(target);
        }

        public void RemoveTarget()
        {
            _targetViewModel = null;
            _disposables?.Dispose();
        }

        private void SubscribeToTargetPosition(ITrackable target)
        {
            _targetViewModel = target;
            _targetViewModel.Position
                .Subscribe(position => transform.position = position)
                .AddTo(_disposables);
        }
    }
}
