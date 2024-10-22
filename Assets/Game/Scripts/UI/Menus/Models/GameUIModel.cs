using Game.Scripts.UI.Base;
using Game.Scripts.UI.Menus.Interfaces;

namespace Game.Scripts.UI.Menus.Models
{
    public class GameUIModel : UIModelBase, IGameUIModel
    {
        public void MenuButtonClicked()
        {
            StateMachine.ChangeStateTo(UIType.Pause);
        }
    }

    public interface IGameUIModel : IUIModel
    {
        public void MenuButtonClicked();
    }
}
