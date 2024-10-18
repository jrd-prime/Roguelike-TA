using System;
using Game.Scripts.Framework.GameStateMachine.State;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIView menuUI;
        [SerializeField] private UIView gameUI;
        [SerializeField] private UIView pauseUI;
        [SerializeField] private UIView deathUI;
        [SerializeField] private UIView settingsUI;

        public void ShowMenu(UIType mainMenu)
        {
            throw new NotImplementedException();
        }
    }
}
