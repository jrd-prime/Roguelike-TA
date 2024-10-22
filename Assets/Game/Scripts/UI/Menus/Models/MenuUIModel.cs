using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Interfaces;
using UnityEngine;

namespace Game.Scripts.UI.Menus.Models
{
    public interface IMenuUIModel : IUIModel
    {
        public void StartButtonClicked();
        public void SettingsButtonClicked();
        public void ExitButtonClicked();
    }

    public class MenuUIModel : UIModelBase, IMenuUIModel
    {
        private const float DoubleClickDelay = 0.5f;
        private float _lastClickTime;

        public void StartButtonClicked()
        {
            StateMachine.ChangeStateTo(UIType.Game);
        }

        public void SettingsButtonClicked()
        {
            StateMachine.ChangeStateTo(UIType.Settings);
        }

        public void ExitButtonClicked()
        {
            var currentTime = Time.time;

            if (currentTime - _lastClickTime < DoubleClickDelay)
                ExitGame();
            else
                _lastClickTime = currentTime;

            UIManager.ShowPopUpAsync("Click 2 times to exit.", (int)(DoubleClickDelay * 2000));
        }

        private static void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
