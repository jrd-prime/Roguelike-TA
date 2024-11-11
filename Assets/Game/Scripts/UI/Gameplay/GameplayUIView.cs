using System;
using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.GamePlay;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Gameplay
{
    [RequireComponent(typeof(Canvas))]
    public class GameplayUIView : UICanvasViewCustom<GameUIViewModel>
    {
        private static readonly int MainValue = Shader.PropertyToID("_MainValue");
        [SerializeField] private Image healthBarImage;

        private TMP_Text _healthBarLabel;

        // health bar
        private int _playerInitialHealth;
        private float _fullHpWidth;
        public bool isFullHpWidthSet;
        private float _pxPerPointHp;
        private float _currentHpBarWidth;

        protected override void InitElements()
        {
            _healthBarLabel = CanvasRoot.GetComponentInChildren<TMP_Text>();

            _healthBarLabel.text = "100 ----- 100";

            if (_healthBarLabel == null) throw new NullReferenceException("_healthBarLabel is null.");
            {
            }
        }

        protected override void Init()
        {
            // Health
            ViewModel.PlayerInitialHealth
                .Subscribe(initialHealth => { _playerInitialHealth = initialHealth; })
                .AddTo(Disposables);

            ViewModel.PlayerHealth.Subscribe(UpdateHealthBar).AddTo(Disposables);
        }


        protected override void InitCallbacksCache()
        {
            // throw new NotImplementedException();
        }

        private void UpdateHealthBar(int health)
        {
            _healthBarLabel.text = health + " / " + _playerInitialHealth;
            var material = healthBarImage.material;
            Debug.LogWarning($"f: {_playerInitialHealth / (float)health}");


            healthBarImage.material.SetFloat(MainValue, (float)health / _playerInitialHealth);


            if (!isFullHpWidthSet) return;
            // _healthBar
            //     .experimental
            //     .animation
            //     .Size(new Vector2(_pxPerPointHp * health, _currentHpBarWidth), 500).Start();
            // _currentHpBarWidth = _pxPerPointHp * health;
        }
    }
}
