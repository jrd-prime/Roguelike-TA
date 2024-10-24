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

        private float _currentWidth;


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
            _currentWidth = 1;
        }

        public void SetHP(float hpProcentage)
        {
            Debug.LogWarning($"Set hp bar {hpProcentage} / hpbar width {_hpBarWidth}");
            hpBarRect.sizeDelta = new Vector2(_hpBarWidth * hpProcentage, _hpBarHeight);
            // hpBar.fillAmount = hp / 100;
            // hpText.text = $"{hp}%";
        }
    }
}
