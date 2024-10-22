namespace Game.Scripts.Framework.Sort.Camera
{
    public interface ICameraController
    {
        void SetFollowTarget(ITrackable target);
        public void RemoveTarget();
    }
}
