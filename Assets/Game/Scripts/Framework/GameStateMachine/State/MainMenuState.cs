using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class MainMenuState : IGameState
    {
        public void Enter()
        {
            Debug.Log("main menu state enter");
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
}
