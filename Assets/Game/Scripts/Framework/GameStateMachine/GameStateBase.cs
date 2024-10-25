using VContainer;

namespace Game.Scripts.Framework.GameStateMachine
{
    public abstract class GameStateBase
    {
        protected GameManager GameManager { get; private set; }


        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            GameManager = resolver.Resolve<GameManager>();
        }
    }
}
