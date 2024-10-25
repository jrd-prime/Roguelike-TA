using Game.Scripts.UI;
using VContainer;

namespace Game.Scripts.Framework.GameStateMachine
{
    public abstract class GameStateBase
    {
        protected UIManager UIManager;
        protected IObjectResolver Resolver;

        [Inject]
        private void Construct(UIManager uiManager, IObjectResolver resolver)
        {
            UIManager = uiManager;
            Resolver = resolver;
        }
    }
}
