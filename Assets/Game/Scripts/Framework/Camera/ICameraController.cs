using Game.Scripts.Framework.Systems.Follow;

namespace Game.Scripts.Framework.Camera
{
    public interface ICameraController
    {
        void SetFollowTarget(ITrackable target);
        public void RemoveTarget();
    }
}
