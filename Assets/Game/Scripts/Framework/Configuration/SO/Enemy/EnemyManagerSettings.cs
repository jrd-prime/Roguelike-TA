using Game.Scripts.Enemy;
using Game.Scripts.Framework.Constants;
using Game.Scripts.Framework.Managers.Settings;
using UnityEngine;

namespace Game.Scripts.Framework.Configuration.SO.Enemy
{
    [CreateAssetMenu(
        fileName = "EnemyManagerSettings",
        menuName = SOPathConst.ConfigPath + "New Enemy Manager Settings",
        order = 100)]
    public class EnemyManagerSettings : SettingsBase
    {
        public override string Description => "Settings for enemy manager";
        public EnemyHolder enemyHolderPrefab;
    }
}
