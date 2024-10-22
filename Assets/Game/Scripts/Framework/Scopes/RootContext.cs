using Game.Scripts.Framework.Providers.AssetProvider;
using Game.Scripts.Framework.Sort.Camera;
using Game.Scripts.Framework.Sort.Configuration;
using Game.Scripts.Framework.Sort.Input;
using Game.Scripts.Framework.Sort.ScriptableObjects;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class RootContext : LifetimeScope
    {
        [SerializeField] private SMainConfig sMainConfig;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private EventSystem eventSystem;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("<color=cyan>ROOT CONTEXT</color>");
            Check(cameraController);


            // Components
            var input = Check(gameObject.AddComponent(typeof(MobileInput)));
            builder.RegisterComponent(input).AsSelf();
            builder.RegisterComponent(cameraController).As<ICameraController>();
            builder.RegisterComponent(eventSystem).AsSelf();

            builder.RegisterInstance(sMainConfig);

            // Services
            builder.Register<ConfigManager>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register(typeof(AssetProvider), Lifetime.Singleton).As<IAssetProvider>();
            // Systems
            builder.Register<FollowSystem>(Lifetime.Singleton).AsSelf();
        }


        private T Check<T>(T component) where T : class
        {
            Assert.IsNotNull(component, $"{typeof(T)} is null. Add config to {this}");
            return component;
        }
    }
}
