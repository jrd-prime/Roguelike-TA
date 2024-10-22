using Game.Scripts.Framework.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyHolder : MonoBehaviour
    {
        public string EnemyID { get; private set; }

        public void SetEnemyId(string toString)
        {
            EnemyID = toString;
        }

        public void Fill(EnemySettings enemiesSettings)
        {
            // fill with settings and prefab
        }

        public void FillEnemySettings(string enemyId, EnemySettings enemiesSettings)
        {
            // throw new System.NotImplementedException();
        }

        public void OnSpawn()
        {
            // Здесь можно добавить код для инициализации врага при спавне.
            Debug.Log("Enemy spawned");
        }

        public void OnDespawn()
        {
            // Код для подготовки врага перед возвратом в пул.
            Debug.Log("Enemy despawned");
        }
    }
}
