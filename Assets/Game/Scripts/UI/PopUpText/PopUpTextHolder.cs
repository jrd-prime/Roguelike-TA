using System;
using DG.Tweening;
using Game.Scripts.Framework.Managers.Camera;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game.Scripts.UI.PopUpText
{
    public class PopUpTextHolder : MonoBehaviour
    {
        [SerializeField] private TMP_Text popUpText;
        private ICameraManager _cameraManager;

        [Inject]
        private void Construct(ICameraManager cameraManager) => _cameraManager = cameraManager;

        private void SetRotationToFace(Camera mainCamera)
        {
            var cameraRotationX = mainCamera.transform.eulerAngles.x;
            var canvasRotation = popUpText.transform.eulerAngles;

            canvasRotation.x = cameraRotationX;
            popUpText.transform.eulerAngles = canvasRotation;
        }

        private void Start()
        {
            if (_cameraManager == null) throw new NullReferenceException("CameraManager is null.");

            SetRotationToFace(_cameraManager.MainCamera);
            if (popUpText == null) throw new NullReferenceException($"PopUpText is null. {popUpText.GetType()}");
        }

        public void Show(string text, float duration, bool isCrit = false)
        {
            Reset();
            SetText(text);
            Animate(duration, isCrit);
        }

        private void Animate(float duration, bool isCrit)
        {
            if (isCrit)
            {
                popUpText.transform.DOMoveY(1f, duration).SetRelative().SetEase(Ease.OutElastic);
                popUpText.transform.DOScale(2f, 0.5f).SetEase(Ease.OutBounce);
            }
            else popUpText.transform.DOMoveY(1f, duration).SetRelative().SetEase(Ease.OutQuad);

            popUpText.DOFade(0, duration);
        }

        private void SetText(string text) => popUpText.text = text;

        private void Reset()
        {
            popUpText.alpha = 1f;
            popUpText.transform.localPosition = new Vector3(0f, 1f, 0f);
            popUpText.transform.localScale = Vector3.one;
        }
    }
}
