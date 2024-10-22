using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.ViewModels;
using R3;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.Views
{
    public class PauseUIView : UIViewCustom<PauseUIViewModel>
    {
        private Button _continueButton;
        private Button _settingsButton;
        private Button _toMainMenuButton;

        protected override void InitElements()
        {
            _continueButton = RootVisualElement.Q<Button>(UIConst.ContinueButtonIDName);
            _settingsButton = RootVisualElement.Q<Button>(UIConst.SettingsButtonIDName);
            _toMainMenuButton = RootVisualElement.Q<Button>(UIConst.MenuButtonIDName);

            CheckOnNull(_continueButton, UIConst.ContinueButtonIDName, name);
            CheckOnNull(_settingsButton, UIConst.SettingsButtonIDName, name);
            CheckOnNull(_toMainMenuButton, UIConst.MenuButtonIDName, name);
        }

        protected override void InitCallbacksCache()
        {
            CallbacksCache.TryAdd(_continueButton, _ => ViewModel.ContinueButtonClicked.OnNext(Unit.Default));
            CallbacksCache.TryAdd(_settingsButton, _ => ViewModel.SettingsButtonClicked.OnNext(Unit.Default));
            CallbacksCache.TryAdd(_toMainMenuButton, _ => ViewModel.ToMainMenuButtonClicked.OnNext(Unit.Default));
        }
    }
}
