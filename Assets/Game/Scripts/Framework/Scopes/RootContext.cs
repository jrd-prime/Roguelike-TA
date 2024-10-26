using Game.Scripts.Framework.Camera;
using Game.Scripts.Framework.Input;
using Game.Scripts.Framework.Managers.Settings;
using Game.Scripts.Framework.Providers.AssetProvider;
using Game.Scripts.Framework.ScriptableObjects;
using Game.Scripts.Framework.Systems.Follow;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class RootContext : LifetimeScope
    {
        [FormerlySerializedAs("sMainConfig")] [SerializeField] private MainSettings mainSettings;
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

            builder.RegisterInstance(mainSettings);

            // Services
            builder.Register<SettingsManager>(Lifetime.Singleton).AsImplementedInterfaces();

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
