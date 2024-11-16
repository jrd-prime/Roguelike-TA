using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using R3;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.GamePlay.Components
{
    public sealed class HealthBar
    {
        private const float Epsilon = 0.001f;
        private const float AnimationDuration = 0.5f;

        private VisualElement _healthBarBg;
        private VisualElement _healthBar;
        private Label _healthBarLabel;
        private int _playerInitialHealth;
        private float _fullHpWidth;
        private bool _isFullHpWidthSet;
        private float _pxPerPointHp;
        private float _currentHpBarWidth;
        private TweenerCore<float, float, FloatOptions> _healthTween;

        private readonly GameUIViewModel _viewModel;
        private readonly VisualElement _root;
        private readonly CompositeDisposable _disposables;

        public HealthBar(in GameUIViewModel viewModel, in VisualElement root, in CompositeDisposable disposables)
        {
            _viewModel = viewModel;
            _root = root;
            _disposables = disposables;
        }

        private void SetHpBarWidth(float width)
        {
            if (_isFullHpWidthSet) return;
            _isFullHpWidthSet = true;
            _fullHpWidth = width;
            _pxPerPointHp = _fullHpWidth / _playerInitialHealth;
            _currentHpBarWidth = _fullHpWidth;
            _healthBar.style.width = new StyleLength(_fullHpWidth);
            UpdateHealthBar(_playerInitialHealth);
        }

        public void UpdateHealthBar(int health)
        {
            _healthBarLabel.text = $"{health} / {_playerInitialHealth}";

            if (!_isFullHpWidthSet) return;

            var targetWidth = _pxPerPointHp * health;

            if (Math.Abs(targetWidth - _currentHpBarWidth) < Epsilon) return;

            _healthTween.Kill();
            _healthTween = DOTween.To(
                () => _currentHpBarWidth,
                x =>
                {
                    _currentHpBarWidth = x;
                    _healthBar.style.width = x;
                },
                targetWidth,
                AnimationDuration
            );
        }

        public void ResetHealthBar()
        {
            _healthTween.Kill();
            _currentHpBarWidth = _fullHpWidth;
            _healthBar.style.width = new StyleLength(_fullHpWidth);
            _healthBarLabel.text = $"{_playerInitialHealth} / {_playerInitialHealth}";
        }

        public void InitElements()
        {
            _healthBarBg = _root.Q<VisualElement>(UIConst.HealthBarContainerIDName);
            _healthBarLabel = _healthBarBg.Q<Label>(UIConst.HealthBarLabelIDName);
            _healthBar = _healthBarBg.Q<VisualElement>(UIConst.HealthBarMoveIDName);
        }

        public void Init()
        {
            _healthBar.RegisterCallback<GeometryChangedEvent>(_ => SetHpBarWidth(_healthBar.resolvedStyle.width));

            _viewModel.PlayerInitialHealth
                .Subscribe(initialHealth => { _playerInitialHealth = initialHealth; })
                .AddTo(_disposables);
        }
    }
}
