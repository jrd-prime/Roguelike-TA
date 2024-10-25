using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Interfaces;
using UnityEngine;

namespace Game.Scripts.UI.Menus.Models
{
    public class PauseUIModel : UIModelBase, IPauseUIModel
    {
        private const float DoubleClickDelay = 0.5f;
        private float _lastClickTime;

        public void ContinueButtonClicked()
        {
            StateMachine.ChangeStateTo(UIType.Game);
        }

        public void SettingsButtonClicked()
        {
            // StateMachine.ChangeStateTo(UIType.Settings);
            UIManager.ShowPopUpAsync("Low priority. Will be implemented later.");
        }

        public void ToMainMenuButtonClicked()
        {
            UIManager.ShowPopUpAsync("Low priority. Will be implemented later.");
            // StateMachine.ChangeStateTo(UIType.Menu);
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

        public override void Initialize()
        {
        }
    }

    public interface IPauseUIModel : IUIModel
    {
        public void ContinueButtonClicked();
        public void SettingsButtonClicked();
        public void ToMainMenuButtonClicked();
        public void ExitButtonClicked();
    }
}
