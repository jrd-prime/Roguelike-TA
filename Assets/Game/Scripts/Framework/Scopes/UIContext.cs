using Game.Scripts.UI;
using Game.Scripts.UI.Base;
using Game.Scripts.UI.Models;
using Game.Scripts.UI.ViewModels;
using Game.Scripts.UI.Views;
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

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("<color=cyan>UI CONTEXT</color>");

            // ViewModels
            builder.Register<MenuUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SettingsUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameOverUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PauseUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Models
            builder.Register<IMenuUIModel, MenuUIModel>(Lifetime.Singleton);
            builder.Register<SettingsUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameOverUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PauseUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Views
            builder.RegisterComponent(menu).As<MenuUIView>();
            builder.RegisterComponent(game).As<GameUIView>();
            builder.RegisterComponent(pause).As<PauseUIView>();
            builder.RegisterComponent(gameOver).As<GameOverUIView>();
            builder.RegisterComponent(settings).As<SettingsUIView>();
        }
    }
}
