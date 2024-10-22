﻿using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Scripts.Framework.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "MainSettings",
        menuName = SOPathConst.ConfigPath + "Main Settings",
        order = 100)]
    public class SMainConfig : ScriptableObject
    {
        [Title("Character Settings")] public CharacterSettings characterSettings;
        [Title("Enemy Manager Settings")] public EnemyManagerSettings enemyManagerSettings;
        [Title("Enemy Settings")] public EnemiesMainSettings enemiesMainSettings;


        private void OnValidate()
        {
            Assert.IsNotNull(characterSettings, "Main Configurations: Character config is null!");
            Assert.IsNotNull(enemyManagerSettings, "Main Configurations: Enemy Manager config is null!");
            Assert.IsNotNull(enemiesMainSettings, "Main Configurations: Enemy config is null!");
        }
    }
}
