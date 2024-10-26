using Game.Scripts.Framework.Bootstrap;
using Game.Scripts.Framework.Bootstrap.UI;
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
            Debug.LogWarning("<color=cyan>BOOSTRAP CONTEXT</color>");

            builder.Register<ILoadingScreenModel, LoadingScreenModel>(Lifetime.Singleton);
            builder.Register<LoadingScreenUIViewModel>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.RegisterComponent(loadingScreenView);
            builder.Register<Loader>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<AppStarter>();
        }
    }
}
