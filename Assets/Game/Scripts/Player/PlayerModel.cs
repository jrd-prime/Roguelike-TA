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
    public class PlayerModel : ITrackable, IInitializable, IDisposable
    {
        public Action<float> TrackableAction { get; private set; }
        public ReactiveProperty<Vector3> Position { get; } = new();
        public ReactiveProperty<Quaternion> Rotation { get; } = new();
        public ReactiveProperty<Vector3> MoveDirection { get; } = new();
        public ReactiveProperty<float> MoveSpeed { get; } = new();
        public ReactiveProperty<float> RotationSpeed { get; } = new();
        public ReactiveProperty<bool> IsMoving { get; } = new();
        public ReactiveProperty<float> Health { get; } = new();


        public CharacterSettings characterSettings { get; private set; }
        public JoystickModel joystick { get; private set; }
        public FollowSystem followSystem { get; private set; }

        private StateMachine _stateMachine;

        [Inject]
        private void Construct(IObjectResolver container)
        {
            joystick = container.Resolve<JoystickModel>();
            followSystem = container.Resolve<FollowSystem>();

            var configManager = container.Resolve<IConfigManager>();
            characterSettings = configManager.GetConfig<CharacterSettings>();
            Assert.IsNotNull(characterSettings, "Character settings not found!");

            _stateMachine = container.Resolve<StateMachine>();
        }

        public void Initialize()
        {
            Debug.LogWarning("Init Char Model");
            TrackableAction = TakeDamage;
            MoveSpeed.Value = characterSettings.moveSpeed;
            RotationSpeed.Value = characterSettings.rotationSpeed;
            SetHealth(characterSettings.health);
        }

        public void SetPosition(Vector3 position) => Position.Value = position;

        public void SetRotation(Quaternion rotation) => Rotation.Value = rotation;

        public void SetHealth(float health) => Health.Value = health;


        public void NewGameStart()
        {
            SetHealth(characterSettings.health);
        }

        public void TakeDamage(float damage)
        {
            var healthValue = Health.Value - damage;

            // TODO create game manager!!!
            if (healthValue <= 0)
            {
                Debug.LogWarning("PLAYER MODEL => GAME OVER");
                _stateMachine.ChangeStateTo(UIType.GameOver);

                SetPosition(Vector3.zero);
                return;
            }

            SetHealth(healthValue);
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
    }
}
