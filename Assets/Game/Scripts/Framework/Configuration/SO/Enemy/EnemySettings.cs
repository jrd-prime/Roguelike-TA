using Game.Scripts.Framework.Constants;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Game.Scripts.Framework.Configuration.SO.Enemy
{
    [CreateAssetMenu(
        fileName = "Custom Enemy",
        menuName = SOPathConst.EnemyPath + "New Enemy",
        order = 100)]
    public class EnemySettings : ScriptableObject
    {
        [Title("Main")] public string enemyName = "Not Set";
        [FormerlySerializedAs("enemyPrefab")] public AssetReferenceGameObject enemySkinPrefab;

        [Title("Life")] public float health = 100;
        [Title("Attack")] public float damage = 10;
        public float attackDelay = 3f;

        [Title("Movement")] public float speed = 5f;
    }
}
