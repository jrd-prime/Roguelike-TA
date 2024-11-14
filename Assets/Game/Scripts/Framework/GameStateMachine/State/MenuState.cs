using Game.Scripts.UI;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public sealed class MenuState : GameStateBase, IGameState
    {
        public void Enter()
        {
            if (GameManager.IsGameStarted.CurrentValue) GameManager.GameOver();

            UIManager.ShowView(StateType.Menu);
        }

        public void Exit()
        {
            UIManager.HideView(StateType.Menu);
        }
    }
}
