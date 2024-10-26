using System;
using Game.Scripts.Dto;
using R3;
using UnityEngine;

namespace Game.Scripts.Player.Interfaces
{
    public interface IPlayerViewModel : IDisposable
    {
        public ReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
        public ReadOnlyReactiveProperty<float> MoveSpeed { get; }
        public ReadOnlyReactiveProperty<Quaternion> Rotation { get; }
        public ReadOnlyReactiveProperty<Vector3> Position { get; }
        public ReadOnlyReactiveProperty<float> RotationSpeed { get; }
        public ReactiveProperty<CharacterActionDto> CharacterAction { get; }
        public ReactiveProperty<bool> IsInAction { get; }
        public ReadOnlyReactiveProperty<bool> IsMoving { get; }
        public ReadOnlyReactiveProperty<bool> IsShooting { get; }
        public ReactiveProperty<bool> IsGameStarted { get; }
        public void SetModelPosition(Vector3 value);
        public void SetModelRotation(Quaternion value);
        public void ShootToTarget(GameObject nearestEnemy);
    }
}
