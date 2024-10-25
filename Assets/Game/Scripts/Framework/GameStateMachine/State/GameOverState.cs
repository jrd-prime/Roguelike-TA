using Game.Scripts.UI;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class GameOverState : GameStateBase, IGameState
    {
        public void Enter()
        {
            GameManager.ShowView(UIType.GameOver);
            GameManager.StopTheGame();
        }

        public void Exit()
        {
            GameManager.HideView(UIType.GameOver);
        }
    }
}
