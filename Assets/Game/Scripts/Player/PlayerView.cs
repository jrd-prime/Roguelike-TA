using Game.Scripts.Framework.Constants;
using Game.Scripts.Framework.Systems;
using Game.Scripts.Player.Interfaces;
using R3;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
    public class PlayerView : MonoBehaviour
    {
        public LayerMask targetLayer;
        public float scanBoxHorizontal = 32f;
        public float scanBoxVertical = 16f;

        private IPlayerViewModel _viewModel;

        private float _moveSpeed;
        private float _rotationSpeed;
        private Animator _animator;
        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private GameObject _nearestEnemy;

        private bool _isShooting;
        private bool _isMovementBlocked;
        private bool _isGameStarted;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IPlayerViewModel viewModel) => _viewModel = viewModel;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = gameObject.GetComponent<Animator>();
            _viewModel.SetModelPosition(_rb.position);
            Subscribe();
        }

        private void FixedUpdate()
        {
            if (!_isGameStarted) return;
            if (!_isShooting)
            {
                _nearestEnemy = FindTargetSystem.FindNearestInBox(transform, scanBoxHorizontal, scanBoxVertical,
                    targetLayer);

                if (_nearestEnemy is not null) _viewModel.ShootToTarget(_nearestEnemy);
            }

            CharacterMovement();
        }

        private void Subscribe()
        {
            _viewModel.Position
                .Subscribe(position => _rb.position = position)
                .AddTo(_disposables);

            _viewModel.MoveSpeed
                .Subscribe(moveSpeed => { _moveSpeed = moveSpeed; })
                .AddTo(_disposables);

            _viewModel.RotationSpeed
                .Subscribe(rotationSpeed => { _rotationSpeed = rotationSpeed; })
                .AddTo(_disposables);

            _viewModel.MoveDirection
                .Subscribe(newDirection =>
                {
                    if (_isMovementBlocked)
                    {
                        _moveDirection = Vector3.zero;
                        _animator.SetFloat(AnimConst.MoveValue, 0.0f);
                    }
                    else
                    {
                        _moveDirection = newDirection;
                        if (newDirection != Vector3.zero) SetAnimatorBool(AnimConst.IsMoving, true);

                        _animator.SetFloat(AnimConst.MoveValue, newDirection.magnitude);
                    }
                })
                .AddTo(_disposables);

            _viewModel.CharacterAction
                .Where(x => x.AnimationParamId != 0)
                .Subscribe(x => SetAnimatorBool(x.AnimationParamId, x.Value))
                .AddTo(_disposables);

            _viewModel.IsInAction
                .Subscribe(isInAction =>
                {
                    _isMovementBlocked = isInAction;
                    SetAnimatorBool(AnimConst.IsInAction, isInAction);
                })
                .AddTo(_disposables);

            _viewModel.IsShooting
                .Subscribe(value =>
                {
                    _isShooting = value;
                })
                .AddTo(_disposables);

            _viewModel.IsGameStarted
                .Subscribe(value => _isGameStarted = value)
                .AddTo(_disposables);
        }

        private void SetAnimatorBool(int animParameterId, bool value)
        {
            if (_animator.GetBool(animParameterId) == value) return;
            _animator.SetBool(animParameterId, value);
        }

        private void CharacterMovement()
        {
            if (_isMovementBlocked) return;
            MoveCharacter();
            RotateCharacter();
        }

        private void MoveCharacter()
        {
            if (_moveDirection == Vector3.zero) return;

            _rb.position += _moveDirection * (_moveSpeed * Time.fixedDeltaTime);
            _viewModel.SetModelPosition(_rb.position);
        }

        private void RotateCharacter()
        {
            if (_moveDirection.sqrMagnitude <= 0) return;

            var rotation = Quaternion.Lerp(
                _rb.rotation,
                Quaternion.LookRotation(_moveDirection, Vector3.up),
                Time.fixedDeltaTime * _rotationSpeed);

            _rb.rotation = rotation;
            _viewModel.SetModelRotation(rotation);
        }

        private void OnDestroy() => _disposables.Dispose();

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(scanBoxHorizontal, 1f, scanBoxVertical));
        }
#endif
    }
}
