using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.Scripts.Enemy
{
    public class EnemyHUD : MonoBehaviour
    {
        [SerializeField] private Image hpBarBackground;
        [SerializeField] private RectTransform hpBarRect;
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
