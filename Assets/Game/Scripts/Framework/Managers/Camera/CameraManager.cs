using System;
using Game.Scripts.Framework.CommonModel;
using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Managers.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera), typeof(AudioListener))]
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        public UnityEngine.Camera MainCamera { get; private set; }

        private ITrackableModel _targetModel;
        private readonly CompositeDisposable _disposables = new();
        private Vector3 _offset;

        private void Awake()
        {
            MainCamera = GetComponent<UnityEngine.Camera>();
            if (MainCamera == null) throw new NullReferenceException($"MainCamera is null. {this}");
            
            _offset = transform.position;
        }

        public void SetTarget(ITrackableModel target)
        {
            if (target == null) throw new ArgumentNullException($"Target is null. {this}");

            transform.position = target.Position.CurrentValue + _offset;

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
                .Subscribe(position => transform.position = position + _offset)
                .AddTo(_disposables);
        }
    }
}
