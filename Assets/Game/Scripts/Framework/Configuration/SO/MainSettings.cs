using Game.Scripts.Framework.Configuration.SO.Character;
using Game.Scripts.Framework.Configuration.SO.Enemy;
using Game.Scripts.Framework.Configuration.SO.Weapon;
using Game.Scripts.Framework.Constants;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Scripts.Framework.Configuration.SO
{
    [CreateAssetMenu(
        fileName = "MainSettings",
        menuName = SOPathConst.ConfigPath + "Main Settings",
        order = 100)]
    public class MainSettings : ScriptableObject
    {
        public CharacterSettings characterSettings;
        public EnemyManagerSettings enemyManagerSettings;
        public EnemiesMainSettings enemiesMainSettings;
        public WeaponSettings weaponSettings;
        public MovementControlSettings movementControlSettings;

        private void OnValidate()
        {
            Assert.IsNotNull(characterSettings, "Main Configurations: Character config is null!");
            Assert.IsNotNull(enemyManagerSettings, "Main Configurations: Enemy Manager config is null!");
            Assert.IsNotNull(enemiesMainSettings, "Main Configurations: Enemy config is null!");
        }
    }
}
