using Game.Scripts.Enemy;
using Game.Scripts.UI;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class GameOverState : GameStateBase, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("gameo over state enter");
            var enemiesManager = Resolver.Resolve<EnemiesManager>();

            UIManager.ShowView(UIType.GameOver);
            enemiesManager.StopTheGame();
        }

        public void Exit()
        {
            Debug.LogWarning("gameo over state exit");
            UIManager.HideView(UIType.GameOver);
        }
    }
}
