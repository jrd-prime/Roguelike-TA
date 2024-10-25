using Game.Scripts.UI;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class GameOverState : GameStateBase, IGameState
    {
        public void Enter()
        {
            UIManager.ShowView(StateType.GameOver);
            GameManager.GameOver();
        }

        public void Exit()
        {
            UIManager.HideView(StateType.GameOver);
        }
    }
}
