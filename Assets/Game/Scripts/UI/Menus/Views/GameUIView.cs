using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.ViewModels;
using R3;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.Views
{
    public class GameUIView : UIViewCustom<GameUIViewModel>
    {
        private Button _menuButton;

        protected override void InitElements()
        {
            _menuButton = RootVisualElement.Q<Button>(UIConst.MenuButtonIDName);

            CheckOnNull(_menuButton, UIConst.MenuButtonIDName, name);
        }

        protected override void InitCallbacksCache()
        {
            CallbacksCache.Add(_menuButton, _ => ViewModel.MenuButtonClicked.OnNext(Unit.Default));
        }
    }
}
