using Game.Scripts.Framework.GameStateMachine;
using VContainer;

namespace Game.Scripts.UI.Base
{
    public abstract class UIModelBase
    {
        protected StateMachine StateMachine;
        protected UIManager UIManager;

        [Inject]
        private void Construct(StateMachine stateMachine, UIManager uiManager)
        {
            StateMachine = stateMachine;
            UIManager = uiManager;
        }
    }
}
