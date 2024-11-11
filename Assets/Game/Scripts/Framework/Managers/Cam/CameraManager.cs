using System;
using Game.Scripts.Framework.CommonModel;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Game.Scripts.Framework.Managers.Cam
{
    public class CameraManager : MonoBehaviour, ICameraManager, IInitializable
    {
        [SerializeField] private Vector3 cameraOffset = new(0, 10, -5);
        [SerializeField] private Camera mainCamera;

        private ITrackableModel _targetModel;
        private readonly CompositeDisposable _disposables = new();

        public void Initialize()
        {
            if (mainCamera == null) throw new NullReferenceException($"MainCamera is null. {this}");
            SetCameraPosition(cameraOffset);
        }

        private void SetCameraPosition(Vector3 newPosition)
        {
            if (mainCamera.transform.position == newPosition) return;
            mainCamera.transform.position = newPosition + cameraOffset;
        }

        public void SetTarget(ITrackableModel target)
        {
            if (target == null) throw new ArgumentNullException($"Target is null. {this}");

            SetCameraPosition(target.Position.CurrentValue);

            if (_targetModel != null) _disposables?.Dispose();
            SubscribeToTargetPosition(target);
        }

        public void RemoveTarget()
        {
            _targetModel = null;
            _disposables?.Dispose();
        }

        public Camera GetMainCamera() => mainCamera;
        public Vector3 GetCamEulerAngles() => mainCamera.transform.eulerAngles;
        public Quaternion GetCamRotation() => mainCamera.transform.rotation;

        private void SubscribeToTargetPosition(ITrackableModel target)
        {
            _targetModel = target;
            _targetModel.Position
                .Subscribe(SetCameraPosition)
                .AddTo(_disposables);
        }
    }
}
