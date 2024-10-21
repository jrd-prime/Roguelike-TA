using Game.Scripts.Framework.Sort.Player;
using UnityEngine;

namespace Game.Scripts.Framework.Sort.Camera
{
    public interface ICameraController
    {
        public UnityEngine.Camera MainCamera { get; }
        void SetFollowTarget(IPlayerViewModel target);
        public void RemoveTarget();
        public void MoveToPosition(Vector3 position);
    }
}
