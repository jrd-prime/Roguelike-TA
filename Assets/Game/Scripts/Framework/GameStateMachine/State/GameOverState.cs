using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class GameOverState : GameStateBase, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("gameo over state enter");
            UIManager.ShowView(UIType.GameOver);
            EnemiesManager.StopTheGame();
            GameManager.isGameStarted = false;
            
            GameManager.StopTheGame();
        }

        public void Exit()
        {
            Debug.LogWarning("gameo over state exit");
            UIManager.HideView(UIType.GameOver);
        }
    }
}
