using Game.Scripts.Framework.Sort.Player;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Sort.Camera
{
    public class FollowSystem
    {
        private ICameraController _cameraController;
        private bool _hasTarget;

        [Inject]
        private void Construct(ICameraController cameraController)
        {
            _cameraController = cameraController;
        }

        public void SetTarget(IPlayerViewModel target)
        {
            Assert.IsNotNull(_cameraController, "CameraController is null");
            
            Debug.LogWarning("SetTarget: " + target);
            // TODO remove this
            if (_hasTarget)
            {
                _cameraController.RemoveTarget();
                _hasTarget = false;
            }
            else
            {
                Assert.IsNotNull(target, "Target is null");
                _cameraController.SetFollowTarget(target);
                _hasTarget = true;
            }
        }
    }
}
