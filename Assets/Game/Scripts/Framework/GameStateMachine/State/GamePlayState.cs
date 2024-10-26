using Game.Scripts.UI;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public sealed class GamePlayState : GameStateBase, IGameState
    {
        public void Enter()
        {
            UIManager.ShowView(StateType.Game);
            GameManager.StartNewGame();
            PlayerModel.SetGameStarted(true);
            
        }

        public void Exit()
        {
            UIManager.HideView(StateType.Game);
            PlayerModel.SetGameStarted(false);
        }
    }
}
