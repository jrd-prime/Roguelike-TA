using Game.Scripts.Framework.CommonModel;

namespace Game.Scripts.Framework.Managers.Camera
{
    public interface ICameraManager
    {
        public void SetTarget(ITrackableModel target);
        public void RemoveTarget();
        public UnityEngine.Camera MainCamera { get; }
    }
}
