using Game.Scripts.UI;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public sealed class GamePlayState : GameStateBase, IGameState
    {
        public void Enter()
        {
            UIManager.ShowView(StateType.Gameplay);
            GameManager.StartNewGame();
            PlayerModel.SetGameStarted(true);
            
        }

        public void Exit()
        {
            UIManager.HideView(StateType.Gameplay);
            PlayerModel.SetGameStarted(false);
        }
    }
}
