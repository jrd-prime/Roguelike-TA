using Game.Scripts.Framework.Systems.Follow;

namespace Game.Scripts.Framework.Managers.Camera
{
    public interface ICameraManager
    {
        public void SetTarget(ITrackable target);
        public void RemoveTarget();
    }
}
