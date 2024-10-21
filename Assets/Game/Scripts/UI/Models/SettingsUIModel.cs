using Game.Scripts.UI.Base;
using Game.Scripts.UI.Interfaces;
using UnityEngine;

namespace Game.Scripts.UI.Models
{
    public interface ISettingsUIModel : IUIModel
    {
        public void MusicButtonClicked();
        public void VfxButtonClicked();
        public void MenuButtonClicked();
    }

    public class SettingsUIModel : UIModelBase, ISettingsUIModel
    {
        public void MusicButtonClicked()
        {
            // TODO : sound manager on/off music
            UIManager.ShowPopUpAsync("Low priority. Will be implemented later.");
        }

        public void VfxButtonClicked()
        {
            // TODO : sound manager on/off vfx
            UIManager.ShowPopUpAsync("Low priority. Will be implemented later.");
        }

        public void MenuButtonClicked()
        {
            StateMachine.ChangeStateTo(UIType.Menu);
        }
    }
}
