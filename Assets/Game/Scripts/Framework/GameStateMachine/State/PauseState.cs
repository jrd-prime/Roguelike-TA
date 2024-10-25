using Game.Scripts.UI;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class PauseState : GameStateBase, IGameState
    {
        public void Enter()
        {
            UIManager.ShowView(StateType.Pause);
            GameManager.Pause();
        }

        public void Exit()
        {
            UIManager.HideView(StateType.Pause);
            GameManager.UnPause();
        }
    }
}
