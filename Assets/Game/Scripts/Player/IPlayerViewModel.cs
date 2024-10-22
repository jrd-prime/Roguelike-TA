using System;
using Game.Scripts.Animation;
using Game.Scripts.Framework.Sort.Camera;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Game.Scripts.Player
{
    public interface IPlayerViewModel : ITrackable, IInitializable, IDisposable
    {
        public ReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
        public ReadOnlyReactiveProperty<float> MoveSpeed { get; }
        public ReadOnlyReactiveProperty<Quaternion> Rotation { get; }
        public ReadOnlyReactiveProperty<float> RotationSpeed { get; }
        public ReactiveProperty<CharacterActionDto> CharacterAction { get; }
        public ReactiveProperty<bool> IsInAction { get; }
        public ReadOnlyReactiveProperty<bool> IsMoving { get; }
        public void SetModelPosition(Vector3 rbPosition);
        public void SetModelRotation(Quaternion rbRotation);
    }
}
