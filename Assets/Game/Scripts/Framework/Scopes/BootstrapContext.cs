using Game.Scripts.Framework.Bootstrap;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class BoostrapContext : LifetimeScope
    {
        [SerializeField] private LoadingScreenView loadingScreenView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Loader>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<LoadingScreenIuiViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            // Entry point
            builder.RegisterEntryPoint<AppStarter>();
        }
    }
}
