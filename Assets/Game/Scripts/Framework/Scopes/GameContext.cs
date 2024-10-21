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
            Debug.LogWarning("<color=cyan>GAME CONTEXT</color>");

            builder.RegisterComponent(uiManager).AsSelf().AsImplementedInterfaces();
            builder.Register<StateMachine>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameState>(Lifetime.Singleton).AsSelf();

            // States
            builder.Register<MenuState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GamePlayState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SettingsState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameOverState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PauseState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
