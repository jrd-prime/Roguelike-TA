using System;
using Game.Scripts.Framework.Configuration.SO.Character;
using Game.Scripts.Framework.Helpers;
using Game.Scripts.Framework.Managers.Settings;
using Game.Scripts.Framework.Managers.Weapon;
using Game.Scripts.Framework.Systems;
using Game.Scripts.Framework.Weapon;
using Game.Scripts.Player.Interfaces;
using Game.Scripts.UI.MovementControl;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Player
{
    public class PlayerModel : IPlayerModel, IInitializable
    {
        #region Reactive Properties

        public ReactiveProperty<Vector3> Position { get; } = new();
        public ReactiveProperty<Quaternion> Rotation { get; } = new();
        public ReactiveProperty<Vector3> MoveDirection { get; } = new();
        public ReactiveProperty<float> MoveSpeed { get; } = new();
        public ReactiveProperty<float> RotationSpeed { get; } = new();
        public ReactiveProperty<bool> IsMoving { get; } = new();
        public ReactiveProperty<int> Health { get; } = new();
        public ReactiveProperty<bool> IsShooting { get; } = new();
        public ReactiveProperty<bool> IsGameStarted { get; } = new();

        #endregion

        public Action<int> TrackableAction { get; private set; }
        public CharacterSettings CharSettings { get; private set; }

        private IMovementControlModel _movementControlModel;
        private CameraFollowSystem _cameraFollowSystem;
        private WeaponManager _weaponManager;
        private WeaponBase _weapon;
        private IObjectResolver _container;
        private readonly CompositeDisposable _disposables = new(); 

        [Inject]
        private void Construct(IObjectResolver container) => _container = container;


        public async void Initialize()
        {
            _movementControlModel = Resolver.ResolveAndCheck<IMovementControlModel>(_container);
            _cameraFollowSystem = Resolver.ResolveAndCheck<CameraFollowSystem>(_container);
            _weaponManager = Resolver.ResolveAndCheck<WeaponManager>(_container);
            var settingsManager = Resolver.ResolveAndCheck<ISettingsManager>(_container);
            CharSettings = settingsManager.GetConfig<CharacterSettings>();

            _cameraFollowSystem.SetTarget(this);
            MoveSpeed.Value = CharSettings.moveSpeed;
            RotationSpeed.Value = CharSettings.rotationSpeed;
            SetHealth(CharSettings.health);

            _weapon = await _weaponManager.GetCharacterWeapon();

            Subscribe();
        }

        private void Subscribe()
        {
            TrackableAction = TakeDamage;
            _weapon.IsShooting
                .Subscribe(value => IsShooting.Value = value)
                .AddTo(_disposables);

            //TODO on drop joystick slow speed down
            _movementControlModel.MoveDirection
                .Subscribe(SetMoveDirection)
                .AddTo(_disposables);
        }

        public void SetPosition(Vector3 position) => Position.Value = position;
        public void SetRotation(Quaternion rotation) => Rotation.Value = rotation;
        public void SetHealth(int health) => Health.Value = health;
        public void SetGameStarted(bool value) => IsGameStarted.Value = value;
        public void ShootToTarget(GameObject nearestEnemy) => _weapon.ShootAtTarget(nearestEnemy);


        public void TakeDamage(int damage)
        {
            Debug.LogWarning("  take damage: " + damage);
            if (damage > 0) SetHealth(Health.Value - damage);
        }

        public void SetMoveDirection(Vector3 moveDirection)
        {
            MoveDirection.Value = moveDirection;
            IsMoving.Value = moveDirection.magnitude > 0;
        }

        public void ResetPlayer()
        {
            SetPosition(Vector3.zero);
            SetHealth(CharSettings.health);
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
