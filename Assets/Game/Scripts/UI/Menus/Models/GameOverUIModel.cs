using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Interfaces;

namespace Game.Scripts.UI.Menus.Models
{
    public class GameOverUIModel : UIModelBase, IGameOverUIModel
    {
        public override void Initialize()
        {
        }

        public void NewGameButtonClicked()
        {
            StateMachine.ChangeStateTo(UIType.Game);
        }

        public void SettingsButtonClicked()
        {
            UIManager.ShowPopUpAsync("Low priority. Will be implemented later.");
            // StateMachine.ChangeStateTo(UIType.Settings);
        }

        public void MenuButtonClicked()
        {
            StateMachine.ChangeStateTo(UIType.Menu);
        }
    }

    public interface IGameOverUIModel : IUIModel
    {
        public void MenuButtonClicked();
        public void SettingsButtonClicked();
        public void NewGameButtonClicked();
    }
}
