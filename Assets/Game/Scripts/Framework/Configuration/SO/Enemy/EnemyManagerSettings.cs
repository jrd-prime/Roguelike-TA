using Game.Scripts.Enemy;
using Game.Scripts.Framework.Constants;
using UnityEngine;

namespace Game.Scripts.Framework.Configuration.SO.Enemy
{
    [CreateAssetMenu(
        fileName = "EnemyManagerSettings",
        menuName = SOPathConst.ConfigPath + "New Enemy Manager Settings",
        order = 100)]
    public class EnemyManagerSettings : ScriptableObject
    {
        public EnemyHolder enemyHolderPrefab;
    }
}
