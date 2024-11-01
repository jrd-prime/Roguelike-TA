using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Framework.Managers.SpawnPoints
{
    public class SpawnPointsManager : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> spawnPoints = new();
        public Vector3 GetRandomSpawnPoint() => spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
    }
}
