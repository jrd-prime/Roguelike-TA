using System;
using Game.Scripts.Framework.Configuration;
using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.ScriptableObjects.Character;
using Game.Scripts.Framework.Systems.Follow;
using Game.Scripts.UI;
using Game.Scripts.UI.Joystick;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Player
{
    public class PlayerModel : IInitializable, IDisposable
    {
        private StateMachine _stateMachine;
        public ReactiveProperty<Vector3> Position { get; } = new();
        public ReactiveProperty<Quaternion> Rotation { get; } = new();
        public ReactiveProperty<Vector3> MoveDirection { get; } = new();
        public ReactiveProperty<float> MoveSpeed { get; } = new();
        public ReactiveProperty<float> RotationSpeed { get; } = new();
        public ReactiveProperty<bool> IsMoving { get; } = new();

        public ReactiveProperty<float> Health { get; private set; } = new();


        public CharacterSettings characterSettings { get; private set; }
        public JoystickModel joystick { get; private set; }
        public FollowSystem followSystem { get; private set; }


        [Inject]
        private void Construct(IObjectResolver container)
        {
            joystick = container.Resolve<JoystickModel>();
            followSystem = container.Resolve<FollowSystem>();

            var configManager = container.Resolve<IConfigManager>();
            characterSettings = configManager.GetConfig<CharacterSettings>();
            Assert.IsNotNull(characterSettings, "Character settings not found!");
        }

        public void Initialize()
        {
            Debug.LogWarning("Init Char Model");
            MoveSpeed.Value = characterSettings.moveSpeed;
            RotationSpeed.Value = characterSettings.rotationSpeed;
            Health.Value = characterSettings.health;
        }

        public void SetPosition(Vector3 position) => Position.Value = position;
        public void SetRotation(Quaternion rotation) => Rotation.Value = rotation;

        public void SetPlayerHealth(float health) => Health.Value = health;
        public void NewGameStart() => Health.Value = characterSettings.health;

        public void TakeDamage(float damage, string from)
        {
            var healthValue = Health.Value - damage;

            SetPlayerHealth(healthValue);
        }

        public void SetMoveDirection(Vector3 moveDirection)
        {
            MoveDirection.Value = moveDirection;
            IsMoving.Value = moveDirection.magnitude > 0;
        }

        public void Dispose()
        {
            Position?.Dispose();
            Rotation?.Dispose();
            MoveDirection?.Dispose();
            MoveSpeed?.Dispose();
            RotationSpeed?.Dispose();
            IsMoving?.Dispose();
        }

        public void ResetPlayer()
        {
            SetPosition(Vector3.zero);
            SetPlayerHealth(characterSettings.health);
        }
    }
}
