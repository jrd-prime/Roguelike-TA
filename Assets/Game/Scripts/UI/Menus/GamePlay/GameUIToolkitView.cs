using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game.Scripts.Framework.Helpers;
using Game.Scripts.UI.Base;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public class GameUIToolkitView : UIToolkitViewCustom<GameUIViewModel>
    {
        // Movement
        private VisualElement _movementRoot;
        private VisualElement _ring;


        // Buttons
        private Button _menuButton;

        // Health
        private VisualElement _healthBarBg;
        private VisualElement _healthBar;

        private Label _healthBarLabel;

        // Kill count
        private Label _killCountLabel;

        // Experience
        private VisualElement _expBarBg;
        private VisualElement _expBar;
        private Label _expBarLabel;
        private Label _lvlLabel;

        // health bar
        private int _playerInitialHealth;
        private float _fullHpWidth;
        public bool isFullHpWidthSet;
        private float _pxPerPointHp;
        private float _currentHpBarWidth;

        // exp bar
        private float _expToNextLevel;
        private float _fullExpWidth;
        public bool isFullExpWidthSet;
        private float _pxPerPointExp;
        private float _currentExpBarWidth;

        private int _killCount = 0;
        private int _killToWin = 0;
        private int currentLevel = 1;
        private TweenerCore<float, float, FloatOptions> a;

        protected override void InitElements()
        {
            var safeZoneOffset = ScreenHelper.GetSafeZoneOffset(800f, 360f);
            RootVisualElement.style.marginLeft = safeZoneOffset.x >= 16 ? safeZoneOffset.x : 16;
            RootVisualElement.style.marginTop = safeZoneOffset.y;


            _movementRoot = RootVisualElement.Q<VisualElement>(UIConst.MovementRootIDName);
            _ring = RootVisualElement.Q<VisualElement>(UIConst.FullScreenRingIDName);

            _menuButton = RootVisualElement.Q<Button>(UIConst.MenuButtonIDName);
            _healthBarBg = RootVisualElement.Q<VisualElement>(UIConst.HealthBarContainerIDName);
            _healthBarLabel = _healthBarBg.Q<Label>(UIConst.HealthBarLabelIDName);
            _healthBar = _healthBarBg.Q<VisualElement>(UIConst.HealthBarMoveIDName);
            _killCountLabel = RootVisualElement.Q<Label>(UIConst.KillCountLabelIDName);

            _expBarBg = RootVisualElement.Q<VisualElement>(UIConst.ExpBarContainerIDName);
            _expBarLabel = _expBarBg.Q<Label>(UIConst.ExpBarLabelIDName);
            _expBar = _expBarBg.Q<VisualElement>(UIConst.ExpBarMoveIDName);
            _lvlLabel = RootVisualElement.Q<Label>(UIConst.LvlLabelIDName);

            CheckOnNull(_movementRoot, UIConst.MovementRootIDName, name);
            CheckOnNull(_ring, UIConst.FullScreenRingIDName, name);

            CheckOnNull(_menuButton, UIConst.MenuButtonIDName, name);
            CheckOnNull(_healthBarBg, UIConst.HealthBarContainerIDName, name);
            CheckOnNull(_healthBarLabel, UIConst.HealthBarLabelIDName, name);
            CheckOnNull(_healthBar, UIConst.HealthBarMoveIDName, name);
            CheckOnNull(_killCountLabel, UIConst.KillCountLabelIDName, name);

            CheckOnNull(_expBarBg, UIConst.ExpBarContainerIDName, name);
            CheckOnNull(_expBarLabel, UIConst.ExpBarLabelIDName, name);
            CheckOnNull(_expBar, UIConst.ExpBarMoveIDName, name);
            CheckOnNull(_lvlLabel, UIConst.LvlLabelIDName, name);
        }

        private void SetHpBarWidth(float width)
        {
            if (isFullHpWidthSet) return;
            isFullHpWidthSet = true;
            _fullHpWidth = width;
            _pxPerPointHp = _fullHpWidth / _playerInitialHealth;
            _currentHpBarWidth = _fullHpWidth;
            UpdateHealthBar(_playerInitialHealth);
        }


        private void SetExpBarWidth(float width)
        {
            if (isFullExpWidthSet) return;
            isFullExpWidthSet = true;
            _fullExpWidth = width;
            _pxPerPointExp = _fullExpWidth / _expToNextLevel;
            _currentExpBarWidth = 0f;
            UpdateExperienceBar(0);
        }

        protected override void Init()
        {
            _healthBar.RegisterCallback<GeometryChangedEvent>(_ => SetHpBarWidth(_healthBar.resolvedStyle.width));
            _expBar.RegisterCallback<GeometryChangedEvent>(_ => SetExpBarWidth(_expBar.resolvedStyle.maxWidth.value));


            _movementRoot.RegisterCallback<PointerDownEvent>(OnPointerDown);
            _movementRoot.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _movementRoot.RegisterCallback<PointerUpEvent>(OnPointerUp);
            _movementRoot.RegisterCallback<PointerOutEvent>(OnPointerCancel);

            ViewModel.IsGameStarted.Where(x => !x).Subscribe(_ => ResetUI()).AddTo(Disposables);


            // Movement
            ViewModel.IsTouchPositionVisible.Subscribe(IsTouchPositionVisible).AddTo(Disposables);
            ViewModel.RingPosition.Subscribe(SetRingPosition).AddTo(Disposables);


            // Health
            ViewModel.PlayerInitialHealth
                .Subscribe(initialHealth => { _playerInitialHealth = initialHealth; })
                .AddTo(Disposables);

            ViewModel.PlayerHealth.Subscribe(UpdateHealthBar).AddTo(Disposables);
            ViewModel.KillCount
                .Subscribe(killCount =>
                {
                    _killCount = killCount;
                    UpdateKillCountLabel();
                })
                .AddTo(Disposables);

            ViewModel.KillToWin
                .Subscribe(killToWin =>
                {
                    _killToWin = killToWin;
                    UpdateKillCountLabel();
                })
                .AddTo(Disposables);

            // Level and experience
            ViewModel.Level.Subscribe(UpdateLevelLabel).AddTo(Disposables);

            ViewModel.ExpToNextLevel
                .Subscribe(expToNextLevel => { _expToNextLevel = expToNextLevel; })
                .AddTo(Disposables);

            ViewModel.Experience
                .Subscribe(UpdateExperienceBar)
                .AddTo(Disposables);
        }

        private void UpdateLevelLabel(int level)
        {
            _lvlLabel.text = currentLevel.ToString();
            currentLevel = level;
        }

        private void ResetUI()
        {
            Debug.LogWarning("RESET GAMEPLAY UI");
        }

        // private void UpdateHealthBar(int health)
        // {
        //     _healthBarLabel.text = health + " / " + _playerInitialHealth;
        //
        //     if (!isFullHpWidthSet) return;
        //
        //     Debug.LogWarning("hp anim GO to " + _pxPerPointHp * health + " from " + _currentHpBarWidth);
        //
        //     _healthBar
        //         .experimental
        //         .animation
        //         .Size(new Vector2(_pxPerPointHp * health, _currentHpBarWidth), 500)
        //         .KeepAlive()
        //         .Start();
        //
        //
        //     _currentHpBarWidth = _pxPerPointHp * health;
        // }
        private void ResetHealthBar()
        {
            a.Kill();
            _currentHpBarWidth = _fullHpWidth;
            _healthBar.style.width = new StyleLength(_fullHpWidth);
            _healthBarLabel.text = $"{_playerInitialHealth} / {_playerInitialHealth}";
        }

        private void ResetGameplayUI()
        {
            ResetHealthBar();
            ResetExperienceBar();
        }

        private void UpdateHealthBar(int health)
        {
            if (health <= 0)
            {
                ResetGameplayUI();
                return;
            }

            _healthBarLabel.text = $"{health} / {_playerInitialHealth}";

            if (!isFullHpWidthSet) return;

            var targetWidth = _pxPerPointHp * health;

            if (Math.Abs(targetWidth - _currentHpBarWidth) < 0.001f) return;

            a.Kill();
            a = DOTween.To(
                () => _currentHpBarWidth,
                x =>
                {
                    _currentHpBarWidth = x;
                    _healthBar.style.width = x;
                },
                targetWidth,
                0.5f
            );
        }

        private void ResetExperienceBar()
        {
            // Остановка текущей анимации
            a.Kill();

            // Сброс ширины полосы опыта и текста
            _currentExpBarWidth = 0f;
            _expBar.style.width = new StyleLength(0f);
            _expBarLabel.text = $"{0} / {_expToNextLevel}";
        }

        private void UpdateExperienceBar(int exp)
        {
            if (exp < 0) exp = 0; // предотвращаем отрицательное значение

            _expBarLabel.text = $"{exp} / {_expToNextLevel}";

            if (!isFullExpWidthSet) return;

            var targetWidth = _pxPerPointExp * exp;

            // Если ширина не изменилась, анимацию не запускаем
            if (Math.Abs(targetWidth - _currentExpBarWidth) < 0.001f) return;

            // Остановка текущей анимации
            a.Kill();

            // Запуск анимации ширины
            a = DOTween.To(
                () => _currentExpBarWidth,
                x =>
                {
                    _currentExpBarWidth = x;
                    _expBar.style.width = x;
                },
                targetWidth,
                0.5f // длительность анимации
            );
        }

        // private void UpdateExperienceBar(int exp)
        // {
        //     _expBarLabel.text = exp + " / " + _expToNextLevel;
        //     if (!isFullExpWidthSet) return;
        //     _pxPerPointExp = _fullExpWidth / _expToNextLevel;
        //     _expBar
        //         .experimental
        //         .animation
        //         .Size(new Vector2(_pxPerPointExp * exp, _currentExpBarWidth), 500)
        //         .KeepAlive()
        //         .Start();
        //     _currentExpBarWidth = _pxPerPointExp * exp;
        // }

        private void UpdateKillCountLabel() => _killCountLabel.text = _killCount + " / " + _killToWin;

        protected override void InitCallbacksCache()
        {
            CallbacksCache.Add(_menuButton, _ => ViewModel.MenuButtonClicked.OnNext(Unit.Default));
        }

        private void SetRingPosition(Vector2 position)
        {
            _ring.style.left = position.x;
            _ring.style.top = position.y;
        }

        private void IsTouchPositionVisible(bool value) =>
            _ring.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;


        private void OnPointerCancel(PointerOutEvent evt) => ViewModel.OnOutEvent(evt);
        private void OnPointerDown(PointerDownEvent evt) => ViewModel.OnDownEvent(evt);
        private void OnPointerMove(PointerMoveEvent evt) => ViewModel.OnMoveEvent(evt);
        private void OnPointerUp(PointerUpEvent evt) => ViewModel.OnUpEvent(evt);
    }
}
