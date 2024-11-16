using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Interfaces;

namespace Game.Scripts.UI.Menus.Pause
{
    public class PauseUIModel : UIModelBase, IPauseUIModel
    {
        public void ContinueButtonClicked()
        {
            StateMachine.ChangeStateTo(StateType.Gameplay);
        }

        public void SettingsButtonClicked()
        {
            // StateMachine.ChangeStateTo(UIType.Settings);
            UIManager.ShowPopUpAsync("Low priority. Will be implemented later.");
        }

        public void ToMainMenuButtonClicked()
        {
            StateMachine.ChangeStateTo(StateType.Menu);
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
    }
}
