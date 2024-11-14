using System;
using System.Collections.Generic;
using Game.Scripts.Framework.Configuration.SO;
using Game.Scripts.Framework.Configuration.SO.Character;
using Game.Scripts.Framework.Configuration.SO.Enemy;
using Game.Scripts.Framework.Configuration.SO.Weapon;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Managers.Settings
{
    public class SettingsManager : ISettingsManager
    {
        public string Description => "Config Manager";
        public Dictionary<Type, object> ConfigsCache { get; } = new();

        private MainSettings _mainSettings;

        [Inject]
        private void Construct(MainSettings mainSettings) => _mainSettings = mainSettings;

        public void LoaderServiceInitialization()
        {
            if (_mainSettings == null) throw new NullReferenceException("Main Settings is null");

            CheckAndAddToCache(_mainSettings.character);
            CheckAndAddToCache(_mainSettings.enemies);
            CheckAndAddToCache(_mainSettings.enemyManager);
            CheckAndAddToCache(_mainSettings.weapon);
            CheckAndAddToCache(_mainSettings.movementControl);
            CheckAndAddToCache(_mainSettings.gameSettings);
        }

        private void CheckAndAddToCache<T>(T settings) where T : SettingsBase
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (!ConfigsCache.TryAdd(typeof(T), settings))
                Debug.Log($"Error. When adding to cache {typeof(T)}");
        }

        public T GetConfig<T>() where T : SettingsBase => ConfigsCache[typeof(T)] as T;
    }
}
