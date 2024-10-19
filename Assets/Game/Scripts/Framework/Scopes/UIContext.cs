using Game.Scripts.UI.Views;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class UIContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MenuViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<MenuModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
