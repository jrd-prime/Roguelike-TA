using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.ViewModels;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.Views
{
    public class GameUIView : UIViewCustom<GameUIViewModel>
    {
        private Button _menuButton;
        private VisualElement _healthBarBg;
        private VisualElement _healthBarHp;
        private Label _healthBarLabel;

        private float _playerInitialHealth;
        private float _fullwidth;
        public bool isFullwidthSet;
        private float _pxPerPoint;
        private float currentBarWidth;

        protected override void InitElements()
        {
            _menuButton = RootVisualElement.Q<Button>(UIConst.MenuButtonIDName);
            _healthBarBg = RootVisualElement.Q<VisualElement>(UIConst.HealthBarContainerIDName);
            _healthBarLabel = _healthBarBg.Q<Label>(UIConst.HealthBarLabelIDName);
            _healthBarHp = _healthBarBg.Q<VisualElement>(UIConst.HealthBarMoveIDName);


            CheckOnNull(_menuButton, UIConst.MenuButtonIDName, name);
            CheckOnNull(_healthBarBg, UIConst.HealthBarContainerIDName, name);
            CheckOnNull(_healthBarLabel, UIConst.HealthBarLabelIDName, name);
            CheckOnNull(_healthBarHp, UIConst.HealthBarMoveIDName, name);
        }

        private void SetHpBarWidth(float width)
        {
            if (isFullwidthSet) return;
            isFullwidthSet = true;
            _fullwidth = width;
            _pxPerPoint = _fullwidth / _playerInitialHealth;
            currentBarWidth = _fullwidth;
        }

        protected override void Init()
        {
            _healthBarHp.RegisterCallback<GeometryChangedEvent>(_ => SetHpBarWidth(_healthBarHp.resolvedStyle.width));
            
            ViewModel.PlayerInitialHealth
                .Subscribe(initialHealth => _playerInitialHealth = initialHealth)
                .AddTo(Disposables);
            
            ViewModel.PlayerHealth
                .Subscribe(health =>
                {
                    _healthBarLabel.text = health + " / " + _playerInitialHealth;
                    if (!isFullwidthSet) return;
                    _healthBarHp
                        .experimental
                        .animation
                        .Size(new Vector2(_pxPerPoint * health, currentBarWidth), 500).Start();
                    currentBarWidth = _pxPerPoint * health;
                })
                .AddTo(Disposables);
        }


        protected override void InitCallbacksCache()
        {
            CallbacksCache.Add(_menuButton, _ => ViewModel.MenuButtonClicked.OnNext(Unit.Default));
        }
    }
}
