using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.ViewModels;
using R3;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.Views
{
    public class GameUIView : UIViewCustom<GameUIViewModel>
    {
        private Button _menuButton;
        private VisualElement _healthBarContainer;
        private Label _healthBarLabel;

        protected override void InitElements()
        {
            _menuButton = RootVisualElement.Q<Button>(UIConst.MenuButtonIDName);
            _healthBarContainer = RootVisualElement.Q<VisualElement>(UIConst.HealthBarContainerIDName);
            _healthBarLabel = _healthBarContainer.Q<Label>(UIConst.HealthBarLabelIDName);


            CheckOnNull(_menuButton, UIConst.MenuButtonIDName, name);
        }

        protected override void Init()
        {
            ViewModel.PlayerHealth.Subscribe(health => _healthBarLabel.text = health.ToString()).AddTo(Disposables);
        }


        protected override void InitCallbacksCache()
        {
            CallbacksCache.Add(_menuButton, _ => ViewModel.MenuButtonClicked.OnNext(Unit.Default));
        }
    }
}
