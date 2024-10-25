using System;
using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Systems.Follow
{
    public interface ITrackable
    {
        public ReactiveProperty<Vector3> Position { get; }
        public Action<float> TrackableAction { get; }
    }
}
