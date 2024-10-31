using System;
using System.Collections.Generic;
using Game.Scripts.Framework.Configuration.SO;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Framework.Managers.Settings
{
    public class SettingsManager : ISettingsManager
    {
        public string Description => "Config Manager";
        public Dictionary<Type, object> ConfigsCache { get; private set; }

        private MainSettings _mainSettings;

        [Inject]
        private void Construct(MainSettings mainSettings) =>
            _mainSettings = mainSettings;

        public void LoaderServiceInitialization()
        {
            ConfigsCache = new Dictionary<Type, object>();

            AddToCache(_mainSettings.characterSettings);
            AddToCache(_mainSettings.enemiesMainSettings);
            AddToCache(_mainSettings.enemyManagerSettings);
            AddToCache(_mainSettings.weaponSettings);
        }

        private void AddToCache(object config)
        {
            if (!ConfigsCache.TryAdd(config.GetType(), config))
                Debug.Log($"Error. When adding to cache {config.GetType()}");
        }

        public T GetConfig<T>() where T : ScriptableObject => ConfigsCache[typeof(T)] as T;
    }
}
