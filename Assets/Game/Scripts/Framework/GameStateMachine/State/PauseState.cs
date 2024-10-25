using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class PauseState : GameStateBase, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("pause state enter");
            GameManager.ShowView(UIType.Pause);
        }

        public void Exit()
        {
            Debug.LogWarning("pause state exit");
            GameManager.HideView(UIType.Pause);
        }
    }
}
