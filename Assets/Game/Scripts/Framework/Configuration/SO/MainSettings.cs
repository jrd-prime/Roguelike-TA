using Game.Scripts.Framework.Configuration.SO.Character;
using Game.Scripts.Framework.Configuration.SO.Enemy;
using Game.Scripts.Framework.Configuration.SO.Weapon;
using Game.Scripts.Framework.Constants;
using Game.Scripts.Framework.Managers.Settings;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Scripts.Framework.Configuration.SO
{
    [CreateAssetMenu(
        fileName = "MainSettings",
        menuName = SOPathConst.ConfigPath + "Main Settings",
        order = 100)]
    public class MainSettings : SettingsBase
    {
        public override string Description => "Main Settings";
        public CharacterSettings character;
        public EnemyManagerSettings enemyManager;
        public EnemiesMainSettings enemies;
        public WeaponSettings weapon;
        public MovementControlSettings movementControl;
        public GameSettings gameSettings;

        private void OnValidate()
        {
            Assert.IsNotNull(character, "Main Configurations: Character config is null!");
            Assert.IsNotNull(enemyManager, "Main Configurations: Enemy Manager config is null!");
            Assert.IsNotNull(enemies, "Main Configurations: Enemy config is null!");
            Assert.IsNotNull(weapon, "Main Configurations: Weapon config is null!");
            Assert.IsNotNull(movementControl, "Main Configurations: Movement Control config is null!");
            Assert.IsNotNull(gameSettings, "Main Configurations: Game Settings config is null!");
        }
    }
}
