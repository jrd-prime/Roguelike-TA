using Game.Scripts.Animation;
using Game.Scripts.Framework.Sort.Camera;
using Game.Scripts.Framework.Sort.Configuration;
using Game.Scripts.Framework.Sort.ScriptableObjects;
using Game.Scripts.UI.Joystick;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Player
{
    public class PlayerViewModel : IPlayerViewModel
    {
        public ReadOnlyReactiveProperty<Vector3> Position => _model.Position;
        public ReadOnlyReactiveProperty<Quaternion> Rotation => _model.Rotation;
        public ReadOnlyReactiveProperty<Vector3> MoveDirection => _model.MoveDirection;
        public ReadOnlyReactiveProperty<bool> IsMoving => _model.IsMoving;
        public ReadOnlyReactiveProperty<float> MoveSpeed => _model.MoveSpeed;
        public ReadOnlyReactiveProperty<float> RotationSpeed => _model.RotationSpeed;
        public ReactiveProperty<CharacterActionDto> CharacterAction { get; } = new();
        public ReactiveProperty<bool> IsInAction { get; } = new(false);


        private IConfigManager _configManager;
        private PlayerModel _model;
        private FollowSystem _followSystem;
        private JoystickModel _joystick;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IObjectResolver container)
        {
            _model = container.Resolve<PlayerModel>();
            _joystick = container.Resolve<JoystickModel>();
            _configManager = container.Resolve<IConfigManager>();
            _followSystem = container.Resolve<FollowSystem>();
        }

        public void Initialize()
        {
            Assert.IsNotNull(_followSystem, $"{_followSystem.GetType()} is null.");
            _followSystem.SetTarget(this);

            Assert.IsNotNull(_configManager, $"{_configManager.GetType()} is null.");
            var characterConfiguration = _configManager.GetConfig<SCharacterConfig>();
            Assert.IsNotNull(characterConfiguration, "Character configuration not found!");

            _model.SetMoveSpeed(characterConfiguration.moveSpeed);
            _model.SetRotationSpeed(characterConfiguration.rotationSpeed);

            Subscribe();
        }

        private void Subscribe()
        {
            //TODO on drop joystick slow speed down
            _joystick.MoveDirection
                .Subscribe(joystickDirection => _model.SetMoveDirection(joystickDirection))
                .AddTo(_disposables);
        }

        public void SetModelPosition(Vector3 rbPosition) => _model.SetPosition(rbPosition);
        public void SetModelRotation(Quaternion rbRotation) => _model.SetRotation(rbRotation);

        public void Dispose() => _disposables.Dispose();
    }
}
