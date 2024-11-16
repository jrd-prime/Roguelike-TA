using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Interfaces;

namespace Game.Scripts.UI.Menus.GameOver
{
    public class GameOverUIModel : UIModelBase, IGameOverUIModel
    {
        public override void Initialize()
        {
        }

        public void NewGameButtonClicked()
        {
            StateMachine.ChangeStateTo(StateType.Gameplay);
        }

        public void SettingsButtonClicked()
        {
            StateMachine.ChangeStateTo(StateType.Settings);
        }

        public void MenuButtonClicked()
        {
            StateMachine.ChangeStateTo(StateType.Menu);
        }
    }

    public interface IGameOverUIModel : IUIModel
    {
        public void MenuButtonClicked();
        public void SettingsButtonClicked();
        public void NewGameButtonClicked();
    }
}
