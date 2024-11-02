using System.Collections.Generic;
using Game.Scripts.Framework.Constants;
using Game.Scripts.Framework.Managers.Settings;
using UnityEngine;

namespace Game.Scripts.Framework.Configuration.SO.Enemy
{
    [CreateAssetMenu(
        fileName = "EnemiesMainSettings",
        menuName = SOPathConst.ConfigPath + "New Enemies Main Settings",
        order = 100)]
    public class EnemiesMainSettings : SettingsBase
    {
        public override string Description => "Enemies Main Settings";
        public List<EnemySettings> enemies = new(); // <EnemySettings>
    }
}
