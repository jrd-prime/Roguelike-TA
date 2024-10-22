using UnityEngine;

namespace Game.Scripts.Framework.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "Custom Enemy",
        menuName = SOPathConst.EnemyPath + "New Enemy",
        order = 100)]
    public class EnemySettings : ScriptableObject
    {
        public string enemyName;
        public GameObject enemyPrefab;
        public float health;
        public float damage;
        public float speed;
    }
}
