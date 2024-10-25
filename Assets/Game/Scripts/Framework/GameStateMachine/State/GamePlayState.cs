using Game.Scripts.UI;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class GamePlayState : GameStateBase, IGameState
    {
        public void Enter()
        {
            GameManager.ShowView(UIType.Game);
            GameManager.StartThegame();
        }

        public void Exit()
        {
            GameManager.HideView(UIType.Game);
        }
    }
}
