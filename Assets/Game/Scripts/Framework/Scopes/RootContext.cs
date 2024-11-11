using System;
using Game.Scripts.Framework.Configuration.SO;
using Game.Scripts.Framework.Input;
using Game.Scripts.Framework.Managers.Settings;
using Game.Scripts.Framework.Providers.AssetProvider;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class RootContext : LifetimeScope
    {
        [SerializeField] private MainSettings mainSettings;
        [SerializeField] private EventSystem eventSystem;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("<color=cyan>ROOT CONTEXT</color>");

            // Components
            var input = Check(gameObject.AddComponent(typeof(MobileInput)));
            builder.RegisterComponent(input).AsSelf();
            builder.RegisterComponent(eventSystem).AsSelf();

            builder.RegisterInstance(mainSettings);

            // Services
            builder.Register<SettingsManager>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register(typeof(AssetProvider), Lifetime.Singleton).As<IAssetProvider>();
        }
        
        private static T Check<T>(T component) where T : class
        {
            if (component == null) throw new NullReferenceException($"{typeof(T)} is null");
            return component;
        }
    }
}
