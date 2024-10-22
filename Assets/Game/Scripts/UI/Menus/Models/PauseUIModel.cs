using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Interfaces;

namespace Game.Scripts.UI.Menus.Models
{
    public class PauseUIModel : UIModelBase, IPauseUIModel
    {
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
            StateMachine.ChangeStateTo(UIType.Menu);
        }
    }

    public interface IPauseUIModel : IUIModel
    {
        public void ContinueButtonClicked();
        public void SettingsButtonClicked();
        public void ToMainMenuButtonClicked();
    }
}
