using Game.Scripts.UI;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.GameStateMachine
{
    public class GameStateBase : IInitializable
    {
        protected GameManager GameManager { get; private set; }
        protected UIManager UIManager { get; private set; }

        [Inject]
        private void Construct(GameManager gameManager, UIManager uiManager)
        {
            GameManager = gameManager;
            UIManager = uiManager;
        }

        public void Initialize()
        {
            Assert.IsNotNull(GameManager, $"Game manager is null. {this}");
            Assert.IsNotNull(UIManager, $"UI manager is null. {this}");
        }
    }
}
