using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.GameStateMachine.State;
using Game.Scripts.Player;
using Game.Scripts.UI;
using Game.Scripts.UI.Joystick;
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


            builder.Register<PlayerViewModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<JoystickViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Model
            builder.Register<PlayerModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<JoystickModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();


            builder.RegisterComponent(uiManager).AsSelf().AsImplementedInterfaces();
            builder.Register<StateMachine>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameStateBase>(Lifetime.Singleton).AsSelf();

            // States
            builder.Register<MenuState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GamePlayState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SettingsState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameOverState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PauseState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
