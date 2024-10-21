using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class PauseState : GameState, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("pause state enter");
            UIManager.ShowView(UIType.Pause);
        }

        public void Update()
        {
            Debug.LogWarning("pause state update");
        }

        public void Exit()
        {
            Debug.LogWarning("pause state exit");
            UIManager.HideView(UIType.Pause);
        }
    }
}
