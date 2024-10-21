using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Sort.Player
{
    /// <summary>
    /// Player data model
    /// </summary>
    public class PlayerModel : IDisposable, IDataModel
    {
        public ReactiveProperty<Vector3> Position { get; } = new();
        public ReactiveProperty<Quaternion> Rotation { get; } = new();
        public ReactiveProperty<Vector3> MoveDirection { get; } = new();
        public ReactiveProperty<float> MoveSpeed { get; } = new();
        public ReactiveProperty<float> RotationSpeed { get; } = new();
        public ReactiveProperty<bool> IsMoving { get; } = new();

        private Dictionary<Enum, Int32> _stats = new();

        public void Initialize()
        {
            // TODO load/create and set defaults
        }

        public void SetPosition(Vector3 position) => Position.Value = position;
        public void SetRotation(Quaternion rotation) => Rotation.Value = rotation;

        public void SetMoveDirection(Vector3 moveDirection)
        {
            MoveDirection.Value = moveDirection;
            IsMoving.Value = moveDirection.magnitude > 0;
        }

        public void SetMoveSpeed(float value) => MoveSpeed.Value = value;
        public void SetRotationSpeed(float value) => RotationSpeed.Value = value;

        public void SetStat(Enum statType, int value)
        {
            if (_stats.ContainsKey(statType))
                _stats[statType] = value;
        }

        public int GetStat(Enum statType)
        {
            if (!_stats.TryGetValue(statType, out var value))
                throw new Exception($"Stat {statType} not found");
            return value;
        }

        public void Dispose()
        {
            Position.Dispose();
            Rotation.Dispose();
            MoveDirection.Dispose();
        }
    }
}
