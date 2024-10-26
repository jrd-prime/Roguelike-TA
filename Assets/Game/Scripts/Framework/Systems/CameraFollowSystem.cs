using Game.Scripts.Framework.CommonModel;
using Game.Scripts.Framework.Managers.Camera;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Systems
{
    public class CameraFollowSystem
    {
        private ICameraManager _cameraManager;
        private bool _hasTarget;

        [Inject]
        private void Construct(ICameraManager cameraManager) => _cameraManager = cameraManager;
        
        public void SetTarget(ITrackableModel target)
        {
            Assert.IsNotNull(_cameraManager, "CameraController is null");

            // TODO remove this
            if (_hasTarget)
            {
                _cameraManager.RemoveTarget();
                _hasTarget = false;
            }
            else
            {
                Assert.IsNotNull(target, "Target is null");
                _cameraManager.SetTarget(target);
                _hasTarget = true;
            }
        }
    }
}
