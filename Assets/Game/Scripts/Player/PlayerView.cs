using Game.Scripts.Animation;
using Game.Scripts.Enemy;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
    public class PlayerView : MonoBehaviour
    {
        private IPlayerViewModel _viewModel;
        private float _moveSpeed;
        private float _rotationSpeed;
        private Animator _animator;
        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private bool _isMovementBlocked;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IPlayerViewModel viewModel) => _viewModel = viewModel;


        private void Awake()
        {
            Assert.IsNotNull(_viewModel,
                $"ViewModel is null. Ensure that \"{this}\" is added to auto-injection in GameSceneContext prefab");

            _rb = GetComponent<Rigidbody>();
            _animator = gameObject.GetComponent<Animator>();

            _viewModel.SetModelPosition(_rb.position);

            Subscribe();
        }

        private void FixedUpdate() => CharacterMovement();

        public float scanRadius = 1f; // Радиус сканирования
        private GameObject nearestEnemy; // Переменная для хранения ближайшего врага

        // private void Update()
        // {
        //     nearestEnemy = FindNearestEnemy();
        //     if (nearestEnemy != null)
        //     {
        //         Debug.Log("Ближайший враг: " + nearestEnemy.name);
        //         // Дополнительные действия с ближайшим врагом
        //     }
        // }

        // private GameObject FindNearestEnemy()
        // {
        //     Vector3 scanCenter = transform.position;
        //     Collider[] hitColliders = Physics.OverlapSphere(scanCenter, scanRadius);
        //
        //     GameObject closestEnemy = null;
        //     float closestDistance = Mathf.Infinity;
        //
        //     foreach (var hitCollider in hitColliders)
        //     {
        //         // Проверяем, есть ли у объекта тег "Enemy"
        //         if (hitCollider.CompareTag("Enemy"))
        //         {
        //             float distanceToEnemy = Vector3.Distance(scanCenter, hitCollider.transform.position);
        //
        //             if (distanceToEnemy < closestDistance)
        //             {
        //                 closestDistance = distanceToEnemy;
        //                 closestEnemy = hitCollider.gameObject;
        //
        //                 var a = closestEnemy.GetComponent<EnemyHolder>();
        //                 Debug.LogWarning($"Enemy found: {a.EnemyID}");
        //                 
        //                 a.gameObject.SetActive(false);
        //             }
        //         }
        //     }
        //
        //     return closestEnemy;
        // }

        // // Визуализация радиуса поиска в редакторе Unity
        // private void OnDrawGizmosSelected()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(transform.position, scanRadius);
        // }

        private void Subscribe()
        {
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
        }

        private void SetAnimatorBool(int animParameterId, bool value)
        {
            if (_animator.GetBool(animParameterId) == value) return;

            Debug.LogWarning(
                $"<color=red>Anim {animParameterId} to {value}. IsInAction = {_isMovementBlocked}</color>");
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
    }
}
