using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class WinState : GameStateBase, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("win state enter");

            UIManager.ShowView(UIType.Win);
            // EnemiesManager.StopTheGame();
            GameManager.isGameStarted = false;
        }

        public void Exit()
        {
            Debug.LogWarning("win state exit");
            UIManager.HideView(UIType.Win);
        }
    }
}
