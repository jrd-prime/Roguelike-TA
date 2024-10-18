using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class MainMenuState : IGameState
    {
        private readonly UIManager _uiManager;

        public MainMenuState(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Enter()
        {
            Debug.Log("main menu state enter");
            _uiManager.ShowMenu(UIType.mainMenu);
        }

        public void Update()
        {
            Debug.Log("main menu state update");
        }

        public void Exit()
        {
            Debug.Log("main menu state exit");
        }
    }

    public enum UIType
    {
        mainMenu,
        gameUI
    }
}
