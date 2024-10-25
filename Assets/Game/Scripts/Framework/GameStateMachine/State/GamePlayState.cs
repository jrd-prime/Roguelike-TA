using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class GamePlayState : GameStateBase, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("game play state enter");

            UIManager.ShowView(UIType.Game);

            if (GameManager.isGameStarted) return;

            StartNewGame();
        }

        private void StartNewGame()
        {
            PlayerModel.NewGameStart();
            EnemiesManager.StartTheGame();
            GameManager.isGameStarted = true;
        }

        public void Exit()
        {
            Debug.LogWarning("game play state exit");
            UIManager.HideView(UIType.Game);
        }
    }
}
