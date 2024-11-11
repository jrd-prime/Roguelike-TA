using System;
using DG.Tweening;
using Game.Scripts.Framework.Managers.Cam;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Enemy
{
    public class EnemyHUD : MonoBehaviour
    {
        [SerializeField] private Image hpBarBackground;
        [SerializeField] private RectTransform hpBarRect;
        [SerializeField] private TMP_Text takeDamageText;

        private Camera _mainCamera;
        private float _hpBarWidth;
        private float _hpBarHeight;
        private bool _isInitialized;
        private ICameraManager _cameraManager;

        public void Initialize(ICameraManager cameraManager)
        {
            _cameraManager = cameraManager;
            _mainCamera = _cameraManager.GetMainCamera();

            _isInitialized = true;
        }

        private void Start()
        {
            if (!_isInitialized) throw new Exception("HUD is not initialized! Use Initialize method.");

            if (hpBarBackground == null) throw new NullReferenceException("HPBarBackground is null.");
            if (hpBarRect == null) throw new NullReferenceException("HPBar is null.");
            _hpBarWidth = hpBarRect.rect.width;
            _hpBarHeight = hpBarRect.rect.height;
            takeDamageText.gameObject.SetActive(false);
            // float cameraRotationX = _mainCamera.transform.eulerAngles.x;
            float cameraRotationX = _cameraManager.GetCamEulerAngles().x;

            var canvasRotation = takeDamageText.transform.eulerAngles;
            canvasRotation.x = cameraRotationX;
            takeDamageText.transform.eulerAngles = canvasRotation;
        }

        private void FixedUpdate()
        {
            FaceCamera();
        }

        private void FaceCamera()
        {
            if (_mainCamera)
            {
                // Debug.LogWarning("camera not null");
                takeDamageText.transform.LookAt(
                    takeDamageText.transform.position + _cameraManager.GetCamRotation() * Vector3.forward,
                    _cameraManager.GetCamRotation() * Vector3.up);
            }
        }

        public void SetHp(float hpPercentage)
        {
            var newWidth = _hpBarWidth * hpPercentage;
            hpBarRect.DOSizeDelta(new Vector2(newWidth, _hpBarHeight), .3f);
        }

        public void ResetHUD()
        {
            hpBarRect.sizeDelta = new Vector2(_hpBarWidth, _hpBarHeight);
            hpBarBackground.gameObject.SetActive(true);
        }

        public void Hide()
        {
            hpBarBackground.gameObject.SetActive(false);
        }
    }
}
