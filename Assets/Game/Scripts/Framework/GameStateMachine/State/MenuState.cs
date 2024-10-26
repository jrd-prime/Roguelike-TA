using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public sealed class MenuState : GameStateBase, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("menu state enter");
            if (GameManager.isGameStarted.CurrentValue) GameManager.GameOver();

            UIManager.ShowView(StateType.Menu);
        }

        public void Exit()
        {
            UIManager.HideView(StateType.Menu);
        }
    }
}
