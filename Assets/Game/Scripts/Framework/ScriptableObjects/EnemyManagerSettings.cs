using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.Framework.ScriptableObjects
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
