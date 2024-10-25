using Game.Scripts.UI;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class GamePlayState : GameStateBase, IGameState
    {
        public void Enter()
        {
            UIManager.ShowView(StateType.Game);
            GameManager.StartThegame();
        }

        public void Exit()
        {
            UIManager.HideView(StateType.Game);
        }
    }
}
