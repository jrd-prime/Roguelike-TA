using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Systems.Follow
{
    public interface ITrackable
    {
        public ReadOnlyReactiveProperty<Vector3> Position { get; }
    }
}
