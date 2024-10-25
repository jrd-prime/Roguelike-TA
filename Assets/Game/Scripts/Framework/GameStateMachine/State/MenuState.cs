using Game.Scripts.UI;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class MenuState : GameStateBase, IGameState
    {
        public void Enter()
        {
            UIManager.ShowView(StateType.Menu);
        }

        public void Exit()
        {
            UIManager.HideView(StateType.Menu);
        }
    }
}
