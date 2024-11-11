using Game.Scripts.UI.Base;
using R3;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.GameOver
{
    public class GameOverUIToolkitView : UIToolkitViewCustom<GameOverUIViewModel>
    {
        private Button _newGameButton;
        private Button _settingsButton;
        private Button _menuButton;

        protected override void Init()
        {
        }

        protected override void InitElements()
        {
            _newGameButton = RootVisualElement.Q<Button>(UIConst.NewGameButtonIDName);
            _settingsButton = RootVisualElement.Q<Button>(UIConst.SettingsButtonIDName);
            _menuButton = RootVisualElement.Q<Button>(UIConst.MenuButtonIDName);
        }

        protected override void InitCallbacksCache()
        {
            CallbacksCache.TryAdd(_newGameButton , _ => ViewModel.NewGameButtonClicked.OnNext(Unit.Default));
            CallbacksCache.TryAdd(_settingsButton, _ => ViewModel.SettingsButtonClicked.OnNext(Unit.Default));
            CallbacksCache.TryAdd(_menuButton, _ => ViewModel.MenuButtonClicked.OnNext(Unit.Default));
        }
    }
}
