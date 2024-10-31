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

        [Title("Life")] public int health = 100;
        [Title("Attack")] public int damage = 10;
        [Range(0.1f, 10)] public float attackDelayInSec = 3;

        [Title("Movement")] public float speed = 5f;

        [Title("Experience")] public int baseExperiencePoints = 10;
    }
}
