using Game.Scripts.UI;
using VContainer;

namespace Game.Scripts.Framework.GameStateMachine
{
    public class GameStateBase
    {
        protected UIManager UIManager;

        [Inject]
        private void Construct(UIManager uiManager)
        {
            UIManager = uiManager;
        }
    }
}
