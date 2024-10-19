using Game.Scripts.UI;
using VContainer;

namespace Game.Scripts.Framework.GameStateMachine
{
    public class GameState
    {
        protected UIManager UIManager;

        [Inject]
        private void Construct(UIManager uiManager)
        {
            UIManager = uiManager;
        }
    }
}
