using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Framework.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "EnemiesMainSettings",
        menuName = SOPathConst.ConfigPath + "New Enemies Main Settings",
        order = 100)]
    public class EnemiesMainSettings : ScriptableObject
    {
        public List<EnemySettings> enemies = new(); // <EnemySettings>
    }
}
