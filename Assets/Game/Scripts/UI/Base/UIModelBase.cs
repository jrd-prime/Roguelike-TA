using Game.Scripts.Framework.GameStateMachine;
using R3;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.UI.Base
{
    public abstract class UIModelBase : IInitializable
    {
        protected StateMachine StateMachine { get; private set; }
        protected UIManager UIManager { get; private set; }
        protected IObjectResolver Container { get; private set; }
        protected GameManager GamwManager { get; private set; }

        protected readonly CompositeDisposable Disposables = new();

        [Inject]
        private void Construct(StateMachine stateMachine, UIManager uiManager, IObjectResolver container)
        {
            StateMachine = stateMachine;
            UIManager = uiManager;
            Container = container;
            GamwManager = Container.Resolve<GameManager>();
        }

        public abstract void Initialize();
    }
}
