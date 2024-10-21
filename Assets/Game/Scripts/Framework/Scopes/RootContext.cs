using BackwoodsLife.Scripts.Framework.Manager.Input;
using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.GameStateMachine.State;
using Game.Scripts.Framework.Providers.AssetProvider;
using Game.Scripts.Framework.Sort.Camera;
using Game.Scripts.UI;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class RootContext : LifetimeScope
    {
        [SerializeField] private CameraController cameraController;

        protected override void Configure(IContainerBuilder builder)
        {
            var input = Check(gameObject.AddComponent(typeof(MobileInput)));
            Debug.LogWarning("<color=cyan>ROOT CONTEXT</color>");
            Check(cameraController);
            builder.RegisterComponent(input).As<IInput>();
            builder.RegisterComponent(cameraController).As<ICameraController>();
            builder.Register(typeof(AssetProvider), Lifetime.Singleton).As<IAssetProvider>();
        }


        private T Check<T>(T component) where T : class
        {
            Assert.IsNotNull(component, $"{typeof(T)} is null. Add config to {this}");
            return component;
        }
    }
}
