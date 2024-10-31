using System;
using R3;
using UnityEngine;

namespace Game.Scripts.Framework.CommonModel
{
    public interface ITrackableModel
    {
        public Action<int> TrackableAction { get; }
        public ReactiveProperty<Vector3> Position { get; }
        public ReactiveProperty<Quaternion> Rotation { get; }

        public void SetPosition(Vector3 position);
        public void SetRotation(Quaternion rotation);
    }
}
