using Game.Scripts.Framework.Providers.AssetProvider;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class RootContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register(typeof(AssetProvider), Lifetime.Singleton).As<IAssetProvider>();
        }
    }
}
