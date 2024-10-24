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
        [SerializeField] private TMP_Text hpText;
        private float _hpBarWidth;
        private float _hpBarHeight;

        private void Awake()
        {
            Assert.IsNotNull(hpBarBackground, $"HPBarBackground is null. Add to {this}");
            Assert.IsNotNull(hpBarRect, $"HPBar is null. Add to {this}");
            Assert.IsNotNull(hpText, $"HPText is null. Add to {this}");

            hpText.text = "";
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
    }
}
