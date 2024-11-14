using Game.Scripts.Framework.CommonModel;
using UnityEngine;

namespace Game.Scripts.Framework.Managers.Cam
{
    public interface ICameraManager
    {
        public void SetTarget(ITrackableModel target);
        public void RemoveTarget();
        public Camera GetMainCamera();
        public Vector3 GetCamEulerAngles();
        public Quaternion GetCamRotation();
    }
}
