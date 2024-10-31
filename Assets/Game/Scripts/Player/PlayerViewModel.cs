using Game.Scripts.Dto;
using Game.Scripts.Player.Interfaces;
using R3;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Player
{
    public class PlayerViewModel : IPlayerViewModel
    {
        public ReadOnlyReactiveProperty<Vector3> Position => _model.Position;
        public ReadOnlyReactiveProperty<Quaternion> Rotation => _model.Rotation;
        public ReadOnlyReactiveProperty<Vector3> MoveDirection => _model.MoveDirection;
        public ReadOnlyReactiveProperty<bool> IsMoving => _model.IsMoving;
        public ReadOnlyReactiveProperty<bool> IsShooting => _model.IsShooting;
        public ReactiveProperty<bool> IsGameStarted => _model.IsGameStarted;
        public ReadOnlyReactiveProperty<float> MoveSpeed => _model.MoveSpeed;
        public ReadOnlyReactiveProperty<float> RotationSpeed => _model.RotationSpeed;

        public ReactiveProperty<CharacterActionDto> CharacterAction { get; } = new();
        public ReactiveProperty<bool> IsInAction { get; } = new(false);

        private IPlayerModel _model;
        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IObjectResolver container) => _model = container.Resolve<IPlayerModel>();

        public void SetModelPosition(Vector3 value) => _model.SetPosition(value);
        public void SetModelRotation(Quaternion value) => _model.SetRotation(value);
        public void ShootToTarget(GameObject nearestEnemy) => _model.ShootToTarget(nearestEnemy);

        public void Dispose() => _disposables.Dispose();
    }
}
