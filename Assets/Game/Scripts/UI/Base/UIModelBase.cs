using Game.Scripts.Framework.GameStateMachine;
using R3;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.UI.Base
{
    public abstract class UIModelBase : IInitializable
    {
        protected StateMachine StateMachine;
        protected UIManager UIManager;
        protected IObjectResolver Container;
        protected readonly CompositeDisposable Disposables = new();

        [Inject]
        private void Construct(StateMachine stateMachine, UIManager uiManager, IObjectResolver container)
        {
            StateMachine = stateMachine;
            UIManager = uiManager;
            Container = container;
        }

        public abstract void Initialize();
    }
}
