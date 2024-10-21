using System;
using Game.Scripts.UI.Base;
using Game.Scripts.UI.ViewModels;
using R3;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Views
{
    public class MenuUIView : UIViewCustom<MenuUIViewModel>
    {
        private Button _startButton;
        private Button _settingsButton;
        private Button _exitButton;

        protected override void InitElements()
        {
            _startButton = RootVisualElement.Q<Button>(UIConst.StartButtonIDName);
            _settingsButton = RootVisualElement.Q<Button>(UIConst.SettingsButtonIDName);
            _exitButton = RootVisualElement.Q<Button>(UIConst.ExitButtonIDName);

            CheckOnNull(_startButton, UIConst.StartButtonIDName, name);
            CheckOnNull(_settingsButton, UIConst.SettingsButtonIDName, name);
            CheckOnNull(_exitButton, UIConst.ExitButtonIDName, name);
        }

        protected override void InitCallbacksCache()
        {

            CallbacksCache.TryAdd(_startButton, _ => ViewModel.StartButtonClicked.OnNext(Unit.Default));
            CallbacksCache.TryAdd(_settingsButton, _ => ViewModel.SettingsButtonClicked.OnNext(Unit.Default));
            CallbacksCache.TryAdd(_exitButton, _ => ViewModel.ExitButtonClicked.OnNext(Unit.Default));
        }
    }
}
