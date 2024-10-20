using Game.Scripts.UI.Models;
using Game.Scripts.UI.ViewModels;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class UIContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Models
            builder.Register<IMenuUIModel, MenuUIModel>(Lifetime.Singleton);
            builder.Register<SettingsUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameOverUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PauseUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameUIModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // ViewModels
            builder.Register<MenuUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SettingsUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameOverUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PauseUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
