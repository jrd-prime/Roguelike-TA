using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.GameStateMachine.State;
using Game.Scripts.Framework.Providers.AssetProvider;
using Game.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class RootContext : LifetimeScope
    {

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("<color=cyan>ROOT CONTEXT</color>");
            builder.Register(typeof(AssetProvider), Lifetime.Singleton).As<IAssetProvider>();
        }
    }
}
