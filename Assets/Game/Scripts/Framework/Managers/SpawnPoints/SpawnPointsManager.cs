using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Framework.Managers.SpawnPoints
{
    public class SpawnPointsManager : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> spawnPoints = new();

        private void Awake()
        {
            Debug.LogWarning($"Spawn points count: {spawnPoints.Count}");
        }

        public Vector3 GetRandomSpawnPointPosition()
        {
            var point = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
            // Debug.LogWarning($"Random spawn point position: {point}");
            return point;
        }
    }
}
