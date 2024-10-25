using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

namespace Game.Scripts.Framework.ScriptableObjects.Enemy
{
    [CreateAssetMenu(
        fileName = "Custom Enemy",
        menuName = SOPathConst.EnemyPath + "New Enemy",
        order = 100)]
    public class EnemySettings : ScriptableObject
    {
        [Title("Main")] public string enemyName = "Not Set";
        public AssetReferenceGameObject enemyPrefab;

        [Title("Life")] public float health = 100;
        [Title("Attack")] public float damage = 10;
        public float attackDelay = 3f;

        [Title("Movement")] public float speed = 5f;

        private void OnValidate()
        {
            Assert.IsNotNull(enemyPrefab, "Enemy Prefab is null.");
        }
    }
}
