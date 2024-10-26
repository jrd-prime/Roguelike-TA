using System;
using Game.Scripts.Framework.CommonModel;
using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Managers.Camera
{
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        [SerializeField] private UnityEngine.Camera mainCamera;

        private ITrackableModel _targetModel;
        private readonly CompositeDisposable _disposables = new();

        private void Awake()
        {
            if (mainCamera == null) throw new NullReferenceException($"MainCamera is null. {this}");
        }

        public void SetTarget(ITrackableModel target)
        {
            if (target == null) throw new ArgumentNullException($"Target is null. {this}");

            transform.position = target.Position.CurrentValue;

            if (_targetModel != null) _disposables?.Dispose();
            SubscribeToTargetPosition(target);
        }

        public void RemoveTarget()
        {
            _targetModel = null;
            _disposables?.Dispose();
        }

        private void SubscribeToTargetPosition(ITrackableModel target)
        {
            _targetModel = target;
            _targetModel.Position
                .Subscribe(position => transform.position = position)
                .AddTo(_disposables);
        }
    }
}
