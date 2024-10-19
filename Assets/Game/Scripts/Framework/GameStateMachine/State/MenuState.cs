using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class MenuState : GameState, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("main menu state enter");
            UIManager.ShowView(UIType.Menu);
        }

        public void Update()
        {
            Debug.LogWarning("main menu state update");
        }

        public void Exit()
        {
            Debug.LogWarning("main menu state exit");
            UIManager.HideView(UIType.Menu);
        }
    }
}
