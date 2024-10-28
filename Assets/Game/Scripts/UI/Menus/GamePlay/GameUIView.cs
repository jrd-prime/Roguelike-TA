using Game.Scripts.Framework.Managers.Experience;
using Game.Scripts.UI.Base;
using R3;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public class GameUIView : UIViewCustom<GameUIViewModel>
    {
        private Button _menuButton;
        private VisualElement _healthBarBg;
        private VisualElement _healthBar;
        private Label _healthBarLabel;
        private Label _killCountLabel;


        private VisualElement _expBarBg;
        private VisualElement _expBar;
        private Label _expBarLabel;
        private Label _lvlLabel;


        private float _playerInitialHealth;
        private float _fullHpWidth;
        public bool isFullHpWidthSet;
        private float _pxPerPointHp;
        private float currentHpBarWidth;

        private float _playerInitialExp;
        private float _fullExpWidth;
        public bool isFullExpWidthSet;
        private float _pxPerPointExp;
        private float currentExpBarWidth;

        private int _killCount = 0;
        private int _killToWin = 0;

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
            currentHpBarWidth = _fullHpWidth;
        }

        private void SetExpBarWidth(float width)
        {
            if (isFullExpWidthSet) return;
            isFullExpWidthSet = true;
            _fullExpWidth = width;
            _pxPerPointExp = _fullExpWidth / _playerInitialExp;
            currentExpBarWidth = _fullExpWidth;
        }

        protected override void Init()
        {
            _healthBar.RegisterCallback<GeometryChangedEvent>(_ => SetHpBarWidth(_healthBar.resolvedStyle.width));
            _expBar.RegisterCallback<GeometryChangedEvent>(_ => SetExpBarWidth(_expBar.resolvedStyle.width));

            ViewModel.PlayerInitialHealth
                .Subscribe(initialHealth => _playerInitialHealth = initialHealth)
                .AddTo(Disposables);

            ViewModel.PlayerHealth
                .Subscribe(health =>
                {
                    _healthBarLabel.text = health + " / " + _playerInitialHealth;
                    if (!isFullHpWidthSet) return;
                    _healthBar
                        .experimental
                        .animation
                        .Size(new Vector2(_pxPerPointHp * health, currentHpBarWidth), 500).Start();
                    currentHpBarWidth = _pxPerPointHp * health;
                })
                .AddTo(Disposables);

            ViewModel.KillCount
                .Subscribe(killCount =>
                {
                    _killCount = killCount;
                    UpdateKillCount();
                })
                .AddTo(Disposables);

            ViewModel.KillToWin
                .Subscribe(killToWin =>
                {
                    _killToWin = killToWin;
                    UpdateKillCount();
                })
                .AddTo(Disposables);

            ExperienceManager.Experience
                .Subscribe(SetExperience)
                .AddTo(Disposables);

            ExperienceManager.Level
                .Subscribe(SetLevel)
                .AddTo(Disposables);
        }

        private void SetLevel(int level) => _lvlLabel.text = level.ToString();

        private void SetExperience(int exp) => _expBarLabel.text = exp + " / " + _playerInitialExp;


        private void UpdateKillCount() => _killCountLabel.text = _killCount + " / " + _killToWin;


        protected override void InitCallbacksCache()
        {
            CallbacksCache.Add(_menuButton, _ => ViewModel.MenuButtonClicked.OnNext(Unit.Default));
        }
    }
}
