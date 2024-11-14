using Game.Scripts.Enemy;
using UnityEngine;

namespace Game.Scripts.Framework.Systems
{
    public static class FindTargetSystem
    {
        public static GameObject FindNearestInBox(Transform scanFrom, float horizontal, float vertical, LayerMask layer)
        {
            GameObject closestEnemy = null;
            var closestDistanceSquared = Mathf.Infinity;
            var halfExtents = new Vector3(horizontal / 2f, 1f, vertical / 2f);
            var scanCenter = scanFrom.position;

            var hitColliders = Physics.OverlapBox(scanCenter, halfExtents, Quaternion.identity, layer);

            foreach (var collider in hitColliders)
            {
                if (!collider.CompareTag("Enemy")) continue;

                var directionToTarget = collider.transform.position - scanCenter;
                var distanceSquared = directionToTarget.sqrMagnitude;
                var dotProduct = Vector3.Dot(scanFrom.forward, directionToTarget.normalized);

                if (!(dotProduct > Mathf.Cos(22.5f * Mathf.Deg2Rad))) continue; // 45 / 2 = 22.5

                if (!(distanceSquared < closestDistanceSquared)) continue;

                closestDistanceSquared = distanceSquared;
                closestEnemy = collider.gameObject;
            }

            return closestEnemy;
        }
    }
}
