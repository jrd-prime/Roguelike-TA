using Game.Scripts.UI;
using VContainer;

namespace Game.Scripts.Framework.GameStateMachine
{
    public class GameStateBase
    {
        protected GameManager GameManager { get; private set; }
        protected UIManager UIManager;


        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            GameManager = resolver.Resolve<GameManager>();
            UIManager = resolver.Resolve<UIManager>();
        }
    }
}
