using Game.Scripts.Animation;
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

        private PlayerModel _model;
        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IObjectResolver container) => _model = container.Resolve<PlayerModel>();

        public void Initialize()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            //TODO on drop joystick slow speed down
            _model.joystick.MoveDirection
                .Subscribe(joystickDirection => _model.SetMoveDirection(joystickDirection))
                .AddTo(_disposables);
        }

        public void SetModelPosition(Vector3 rbPosition) => _model.SetPosition(rbPosition);
        public void SetModelRotation(Quaternion rbRotation) => _model.SetRotation(rbRotation);

        public void Dispose() => _disposables.Dispose();
    }
}
