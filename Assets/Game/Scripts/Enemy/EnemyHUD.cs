using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.Scripts.Enemy
{
    public class EnemyHUD : MonoBehaviour
    {
        [SerializeField] private Image hpBarBackground;
        [SerializeField] private RectTransform hpBarRect;
        [SerializeField] private TMP_Text takeDamageText;
        [SerializeField] private Camera mainCamera;
        private float _hpBarWidth;
        private float _hpBarHeight;

        private void Awake()
        {
            Assert.IsNotNull(hpBarBackground, $"HPBarBackground is null. Add to {this}");
            Assert.IsNotNull(hpBarRect, $"HPBar is null. Add to {this}");
        }

        private void Start()
        {
            _hpBarWidth = hpBarRect.rect.width;
            _hpBarHeight = hpBarRect.rect.height;
            takeDamageText.gameObject.SetActive(false);
            float cameraRotationX = mainCamera.transform.eulerAngles.x;

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
            if (mainCamera)
            {
                // Debug.LogWarning("camera not null");
                takeDamageText.transform.LookAt(
                    takeDamageText.transform.position + mainCamera.transform.rotation * Vector3.forward,
                    mainCamera.transform.rotation * Vector3.up);
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

        public async void ShowTakeDamage(float damage)
        {
            
        }
    }
}
