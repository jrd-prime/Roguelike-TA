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
            Debug.Log("<color=cyan>BOOSTRAP CONTEXT</color>");

            builder.Register<ILoadingScreenModel, LoadingScreenModel>(Lifetime.Singleton);
            builder.Register<ILoadingScreenViewModel, LoadingScreenViewModel>(Lifetime.Singleton);

            builder.RegisterComponent(loadingScreenView);
            builder.Register<ILoader, Loader>(Lifetime.Singleton);

            builder.RegisterEntryPoint<AppStarter>();
        }
    }
}
