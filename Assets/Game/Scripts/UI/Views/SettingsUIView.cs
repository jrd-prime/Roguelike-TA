using Game.Scripts.UI.Base;
using Game.Scripts.UI.ViewModels;
using R3;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Views
{
    public class SettingsUIView : UIViewCustom<SettingsUIViewModel>
    {
        private Button _menuButton;
        private Button _musicButton;
        private Button _vfxButton;

        protected override void InitElements()
        {
            _menuButton = RootVisualElement.Q<Button>(UIConst.MenuButtonIDName);
            _musicButton = RootVisualElement.Q<Button>(UIConst.MusicButtonIDName);
            _vfxButton = RootVisualElement.Q<Button>(UIConst.VfxButtonIDName);

            CheckOnNull(_menuButton, UIConst.MenuButtonIDName, name);
            CheckOnNull(_musicButton, UIConst.MusicButtonIDName, name);
            CheckOnNull(_vfxButton, UIConst.VfxButtonIDName, name);
        }

        protected override void InitCallbacksCache()
        {
            CallbacksCache.Add(_menuButton, _ => ViewModel.MenuButtonClicked.OnNext(Unit.Default));
            CallbacksCache.Add(_musicButton, _ => ViewModel.MusicButtonClicked.OnNext(Unit.Default));
            CallbacksCache.Add(_vfxButton, _ => ViewModel.VfxButtonClicked.OnNext(Unit.Default));
        }
    }
}
