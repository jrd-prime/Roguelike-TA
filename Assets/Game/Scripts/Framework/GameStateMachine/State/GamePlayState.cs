using Game.Scripts.Enemy;
using Game.Scripts.Player;
using Game.Scripts.UI;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class GamePlayState : GameStateBase, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("game play state enter");
            // var playerModel = Resolver.Resolve<PlayerModel>();

            // playerModel.NewGameStart();
            UIManager.ShowView(UIType.Game);

            // var enemiesManager = Resolver.Resolve<EnemiesManager>();
            // enemiesManager.StartTheGame();
        }

        public void Exit()
        {
            Debug.LogWarning("game play state exit");
            UIManager.HideView(UIType.Game);
        }
    }
}
