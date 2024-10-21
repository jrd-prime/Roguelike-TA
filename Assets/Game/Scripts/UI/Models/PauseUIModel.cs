using Game.Scripts.UI.Base;
using Game.Scripts.UI.Interfaces;

namespace Game.Scripts.UI.Models
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
            // StateMachine.ChangeStateTo(UIType.Menu);
            UIManager.ShowPopUpAsync("Low priority. Will be implemented later.");
        }
    }

    public interface IPauseUIModel : IUIModel
    {
        public void ContinueButtonClicked();
        public void SettingsButtonClicked();
        public void ToMainMenuButtonClicked();
    }
}
