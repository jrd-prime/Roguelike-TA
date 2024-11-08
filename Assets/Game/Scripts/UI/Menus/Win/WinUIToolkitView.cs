using Game.Scripts.UI.Base;
using R3;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.Win
{
    public class WinUIToolkitView : UIToolkitViewCustom<WinUIViewModel>
    {
        private Button _menuButton;
        private Button _startButton;
        private Button _exitButton;

        protected override void Init()
        {
        }

        protected override void InitElements()
        {
            _menuButton = RootVisualElement.Q<Button>(UIConst.MenuButtonIDName);
            _startButton = RootVisualElement.Q<Button>(UIConst.StartButtonIDName);
            _exitButton = RootVisualElement.Q<Button>(UIConst.ExitButtonIDName);

            CheckOnNull(_menuButton, UIConst.MenuButtonIDName, name);
            CheckOnNull(_startButton, UIConst.MusicButtonIDName, name);
            CheckOnNull(_exitButton, UIConst.VfxButtonIDName, name);
        }

        protected override void InitCallbacksCache()
        {
            CallbacksCache.Add(_menuButton, _ => ViewModel.MenuButtonClicked.OnNext(Unit.Default));
            CallbacksCache.Add(_startButton, _ => ViewModel.StartButtonClicked.OnNext(Unit.Default));
            CallbacksCache.Add(_exitButton, _ => ViewModel.ExitButtonClicked.OnNext(Unit.Default));
        }
    }
}
