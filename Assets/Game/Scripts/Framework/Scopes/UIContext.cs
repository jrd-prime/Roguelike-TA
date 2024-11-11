using Game.Scripts.UI.Base;
using Game.Scripts.UI.Gameplay;
using Game.Scripts.UI.Menus.GameOver;
using Game.Scripts.UI.Menus.GamePlay;
using Game.Scripts.UI.Menus.MainMenu;
using Game.Scripts.UI.Menus.Pause;
using Game.Scripts.UI.Menus.Settings;
using Game.Scripts.UI.Menus.Win;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class UIContext : LifetimeScope
    {
        [SerializeField] private UIViewBase menu;
        [SerializeField] private UIViewBase game;
        [SerializeField] private UIViewBase pause;
        [SerializeField] private UIViewBase gameOver;
        [SerializeField] private UIViewBase settings;
        [SerializeField] private UIViewBase win;
        [SerializeField] private UIViewBase gameplayUIView;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("<color=cyan>UI CONTEXT</color>");

            // Models
            builder.Register<IMenuUIModel, MenuUIModel>(Lifetime.Singleton);
            builder.Register<SettingsUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameOverUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PauseUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<WinUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // ViewModels
            builder.Register<MenuUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SettingsUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameOverUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PauseUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<WinUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();


            // Views
            builder.RegisterComponent(menu).As<MenuUIToolkitView>();
            builder.RegisterComponent(game).As<GameUIToolkitView>();
            builder.RegisterComponent(pause).As<PauseUIToolkitView>();
            builder.RegisterComponent(gameOver).As<GameOverUIToolkitView>();
            builder.RegisterComponent(settings).As<SettingsUIToolkitView>();
            builder.RegisterComponent(win).As<WinUIToolkitView>();
            builder.RegisterComponent(gameplayUIView).As<GameplayUIView>();
        }
    }
}
