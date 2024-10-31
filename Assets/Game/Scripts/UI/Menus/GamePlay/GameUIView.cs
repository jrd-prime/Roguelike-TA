using Game.Scripts.UI.Base;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public class GameUIView : UIViewCustom<GameUIViewModel>
    {
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

        protected override void InitElements()
        {
            _menuButton = RootVisualElement.Q<Button>(UIConst.MenuButtonIDName);
            _healthBarBg = RootVisualElement.Q<VisualElement>(UIConst.HealthBarContainerIDName);
            _healthBarLabel = _healthBarBg.Q<Label>(UIConst.HealthBarLabelIDName);
            _healthBar = _healthBarBg.Q<VisualElement>(UIConst.HealthBarMoveIDName);
            _killCountLabel = RootVisualElement.Q<Label>(UIConst.KillCountLabelIDName);

            _expBarBg = RootVisualElement.Q<VisualElement>(UIConst.ExpBarContainerIDName);
            _expBarLabel = _expBarBg.Q<Label>(UIConst.ExpBarLabelIDName);
            _expBar = _expBarBg.Q<VisualElement>(UIConst.ExpBarMoveIDName);
            _lvlLabel = RootVisualElement.Q<Label>(UIConst.LvlLabelIDName);


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
            Debug.LogWarning("===== == == == =  Init GameUIView");

            _healthBar.RegisterCallback<GeometryChangedEvent>(_ => SetHpBarWidth(_healthBar.resolvedStyle.width));
            _expBar.RegisterCallback<GeometryChangedEvent>(_ => SetExpBarWidth(_expBar.resolvedStyle.width));

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

        private void UpdateHealthBar(int health)
        {
            _healthBarLabel.text = health + " / " + _playerInitialHealth;
            if (!isFullHpWidthSet) return;
            _healthBar
                .experimental
                .animation
                .Size(new Vector2(_pxPerPointHp * health, _currentHpBarWidth), 500).Start();
            _currentHpBarWidth = _pxPerPointHp * health;
        }

        private void UpdateExperienceBar(int exp)
        {
            _expBarLabel.text = exp + " / " + _expToNextLevel;
            if (!isFullExpWidthSet) return;
            _pxPerPointExp = _fullExpWidth / _expToNextLevel;
            _expBar
                .experimental
                .animation
                .Size(new Vector2(_pxPerPointExp * exp, _currentExpBarWidth), 500).Start();
            _currentExpBarWidth = _pxPerPointExp * exp;
        }

        private void UpdateKillCountLabel() => _killCountLabel.text = _killCount + " / " + _killToWin;

        protected override void InitCallbacksCache()
        {
            CallbacksCache.Add(_menuButton, _ => ViewModel.MenuButtonClicked.OnNext(Unit.Default));
        }
    }
}
