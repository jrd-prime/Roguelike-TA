using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.GameStateMachine.State;
using Game.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class GameContext : LifetimeScope
    {
        [SerializeField] private UIManager uiManager;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(uiManager).AsSelf().AsImplementedInterfaces();

            builder.Register<StateMachine>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameState>(Lifetime.Singleton).AsSelf();

            // UI States

            builder.Register<MenuState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
