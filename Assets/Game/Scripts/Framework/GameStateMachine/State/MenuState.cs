using Game.Scripts.UI;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class MenuState : GameStateBase, IGameState
    {
        public void Enter()
        {
            GameManager.ShowView(UIType.Menu);
        }

        public void Exit()
        {
            GameManager.HideView(UIType.Menu);
        }
    }
}
