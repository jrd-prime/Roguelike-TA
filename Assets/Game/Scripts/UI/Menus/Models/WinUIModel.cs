using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Interfaces;
using UnityEngine;

namespace Game.Scripts.UI.Menus.Models
{
    public interface IWinUIModel : IUIModel
    {
        public void StartButtonClicked();
        public void ExitButtonClicked();
        public void MenuButtonClicked();
    }

    public class WinUIModel : UIModelBase, IWinUIModel
    {
        private const float DoubleClickDelay = 0.5f;
        private float _lastClickTime;

        public void StartButtonClicked()
        {
            // TODO : sound manager on/off music
            StateMachine.ChangeStateTo(UIType.Game);
        }

// TODO DRY menuUI
        public void ExitButtonClicked()
        {
            var currentTime = Time.time;

            if (currentTime - _lastClickTime < DoubleClickDelay)
                ExitGame();
            else
                _lastClickTime = currentTime;

            UIManager.ShowPopUpAsync("Click 2 times to exit.", (int)(DoubleClickDelay * 2000));
        }

        // TODO DRY menuUI
        private static void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        public void MenuButtonClicked()
        {
            StateMachine.ChangeStateTo(UIType.Menu);
        }

        public override void Initialize()
        {
        }
    }
}
