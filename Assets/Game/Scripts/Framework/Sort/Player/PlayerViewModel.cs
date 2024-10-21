using Cysharp.Threading.Tasks;
using Game.Scripts.Framework.Sort.Camera;
using Game.Scripts.Framework.Sort.Configuration;
using Game.Scripts.Framework.Sort.Joystick;
using Game.Scripts.Framework.Sort.ScriptableObjects;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Sort.Player
{
    public class PlayerViewModel : IPlayerViewModel
    {
        public ReadOnlyReactiveProperty<Vector3> Position => _model.Position;
        public ReadOnlyReactiveProperty<Quaternion> Rotation => _model.Rotation;
        public ReadOnlyReactiveProperty<Vector3> MoveDirection => _model.MoveDirection;
        public ReadOnlyReactiveProperty<bool> IsMoving => _model.IsMoving;
        public ReadOnlyReactiveProperty<float> MoveSpeed => _model.MoveSpeed;
        public ReadOnlyReactiveProperty<float> RotationSpeed => _model.RotationSpeed;
        public ReactiveProperty<CharacterAction> CharacterAction { get; } = new();
        public ReactiveProperty<string> CancelCharacterAction { get; } = new();
        public ReactiveProperty<bool> IsInAction { get; } = new(false);


        private IConfigManager _configManager;
        private PlayerModel _model;
        private FollowSystem _followSystem;
        private JoystickModel _joystick;
        private readonly CompositeDisposable _disposables = new();
        private readonly CharacterAction _characterActionReset = new() { AnimationParamId = 0, Value = false };

        [Inject]
        private void Construct(PlayerModel playerModel, JoystickModel joystickModel, IConfigManager saveAndLoadManager,
            FollowSystem followSystem)
        {
            _model = playerModel;
            _joystick = joystickModel;
            _configManager = saveAndLoadManager;
            _followSystem = followSystem;
        }

        public void Initialize()
        {
            Assert.IsNotNull(_followSystem, $"{_followSystem.GetType()} is null.");
            _followSystem.SetTarget(this);

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
                .Subscribe(joystickDirection => { _model.SetMoveDirection(joystickDirection); })
                .AddTo(_disposables);
        }

        public void SetModelPosition(Vector3 rbPosition) => _model.SetPosition(rbPosition);
        public void SetModelRotation(Quaternion rbRotation) => _model.SetRotation(rbRotation);

        public void Dispose() => _disposables.Dispose();


        private async UniTask NewActionAsync(int animParameterId, int actionDelay)
        {
            Debug.LogWarning("NewActionAsync called. AnimParameterId: " + animParameterId + " ActionDelay: " +
                             actionDelay);
            CharacterAction.Value = new CharacterAction { AnimationParamId = animParameterId, Value = true };
            await UniTask.Delay(actionDelay);
            CharacterAction.Value = new CharacterAction { AnimationParamId = animParameterId, Value = false };

            // reset // TODO bad
            CharacterAction.Value = _characterActionReset;
        }
    }

    public struct CharacterAction
    {
        public int AnimationParamId;
        public bool Value;
    }
}
