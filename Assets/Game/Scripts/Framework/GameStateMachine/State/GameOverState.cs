using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class GameOverState : GameState, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("gameo over state enter");
            UIManager.ShowView(UIType.GameOver);
        }

        public void Update()
        {
            Debug.LogWarning("gameo over state update");
        }

        public void Exit()
        {
            Debug.LogWarning("gameo over state exit");
            UIManager.HideView(UIType.GameOver);
        }
    }
}
