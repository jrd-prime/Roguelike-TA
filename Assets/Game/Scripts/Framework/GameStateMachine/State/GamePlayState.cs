using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class GamePlayState : GameStateBase, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("game play state enter");
            UIManager.ShowView(UIType.Game);
        }

        public void Exit()
        {
            Debug.LogWarning("game play state exit");
            UIManager.HideView(UIType.Game);
        }
    }
}
