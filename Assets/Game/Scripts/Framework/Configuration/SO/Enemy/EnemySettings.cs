using Game.Scripts.Framework.Constants;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Scripts.Framework.Configuration.SO.Enemy
{
    [CreateAssetMenu(
        fileName = "Custom Enemy",
        menuName = SOPathConst.EnemyPath + "New Enemy",
        order = 100)]
    public class EnemySettings : ScriptableObject
    {
        public string enemyName = "Not Set";
        public AssetReferenceGameObject enemySkinPrefab;

        public int health = 100;
        public int damage = 10;
        [Range(0.1f, 10)] public float attackDelayInSec = 3;

        public float speed = 5f;

        public int baseExperiencePoints = 10;

        public float disappearanceDuration = 1.5f;

        public float attackDistance = 1f;
        public float rotationSpeed = 10f;
    }
}
