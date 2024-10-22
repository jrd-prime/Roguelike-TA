using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Sort.Camera
{
    public interface ITrackable
    {
        public ReadOnlyReactiveProperty<Vector3> Position { get; }
    }
}
