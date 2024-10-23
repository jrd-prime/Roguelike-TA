using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Animation;
using Game.Scripts.Enemy;
using Game.Scripts.Framework;
using Game.Scripts.Framework.Configuration;
using Game.Scripts.Framework.Providers.AssetProvider;
using Game.Scripts.Framework.ScriptableObjects.Character;
using Game.Scripts.Framework.ScriptableObjects.Weapon;
using Game.Scripts.Framework.Weapon;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using VContainer;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
    public class PlayerView : MonoBehaviour
    {
        [FormerlySerializedAs("shootFromPoint")]
        public GameObject weaponHoldPont;

        private IPlayerViewModel _viewModel;
        private float _moveSpeed;
        private float _rotationSpeed;
        private Animator _animator;
        private Rigidbody _rb;
        private Vector3 _moveDirection;
        private bool _isMovementBlocked;
        public float scanRadius;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Construct(IPlayerViewModel viewModel, IAssetProvider assetProvider, IConfigManager configManager)
        {
            _viewModel = viewModel;
            _assetProvider = assetProvider;
            _configManager = configManager;
        }


        private async void Awake()
        {
            Assert.IsNotNull(_viewModel,
                $"ViewModel is null. Ensure that \"{this}\" is added to auto-injection in GameContext prefab");

            _rb = GetComponent<Rigidbody>();
            _animator = gameObject.GetComponent<Animator>();

            _viewModel.SetModelPosition(_rb.position);


            var settings = _configManager.GetConfig<CharacterSettings>().weapon;

            var weaponGO =
                await _assetProvider.InstantiateAsync(settings.weaponPrefabReference, weaponHoldPont.transform);
            weapon = weaponGO.GetComponent<WeaponBase>();
            var projectileObj = await _assetProvider.InstantiateAsync(settings.projectilePrefabReference);

            projectileObj.SetActive(false);
            var projectile = projectileObj.GetComponent<Projectile>();
            projectilePool =
                new CustomPool<Projectile>(projectile, 100, null);


            Subscribe();
        }

        public WeaponBase weapon { get; set; }

        public CustomPool<Projectile> projectilePool;

        private void Start()
        {
            _charGun = gameObject.GetComponent<CharacterGun>();
        }

        private GameObject nearestEnemy; // Переменная для хранения ближайшего врага
        private CharacterGun _charGun;
        private bool isShooting = false;
        private IAssetProvider _assetProvider;
        private IConfigManager _configManager;
        private Vector3 _nearestEnemyS;

        private void FixedUpdate()
        {
            // Debug.LogWarning($"FixedUpdate {projectilePool}");

            if (!isShooting) // Проверяем, что не выполняется уже стрельба
            {
                nearestEnemy = FindNearestEnemy();
                if (nearestEnemy != null)
                {
                    // Запускаем стрельбу
                    ShootAtTarget(nearestEnemy);
                }
            }

            CharacterMovement();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(55, 55, 55, 0.2f);
            Gizmos.DrawSphere(_nearestEnemyS + new Vector3(0, 0.5f, 0), 1f);
        }

        private async void ShootAtTarget(GameObject nearestEnemy)
        {
            isShooting = true;

            _nearestEnemyS = nearestEnemy.transform.position;
            var projectile = projectilePool.Get();

            Debug.LogWarning($"Shoot at {nearestEnemy.transform.position}");


            weapon.Shoot(nearestEnemy.transform.position, projectile);

            await UniTask.Delay(2000); // Задержка

            isShooting = false; // После завершения сбрасываем флаг
            // Дополнительные действия после задержки, например, стрельба по следующей цели
        }


        private GameObject FindNearestEnemy()
        {
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;


            Vector3 scanCenter = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(scanCenter, scanRadius);

            foreach (Collider hitCollider in hitColliders)
            {
                if (!hitCollider.CompareTag("Enemy")) continue;

                Vector3 directionToTarget = (hitCollider.transform.position - scanCenter).normalized;

                // Вычисляем угол между forwardDirection и directionToTarget
                float angle = Vector3.Angle(transform.forward, directionToTarget);

                // Проверяем, находится ли цель в пределах угла конуса
                if (angle <= 135 / 2)
                {
                    float distanceToEnemy = Vector3.Distance(scanCenter, hitCollider.transform.position);

                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = hitCollider.gameObject;
                        var id = closestEnemy.GetComponent<EnemyHolder>().EnemyID;
                        Debug.LogWarning($"closestEnemy position when find = {closestEnemy.transform.position} / id = {id}");


                        // var a = closestEnemy.GetComponent<EnemyHolder>();
                    }
                }
            }

            return closestEnemy;
        }


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
