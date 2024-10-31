using UnityEngine;

namespace Game.Scripts.Framework.Systems
{
    public static class FindTargetSystem
    {
        public static GameObject FindNearestInBox(Transform scanFrom, float horizontal, float vertical, LayerMask layer)
        {
            GameObject closestEnemy = null;
            float closestDistanceSquared = Mathf.Infinity;
            Vector3 halfExtents = new Vector3(horizontal / 2f, 1f, vertical / 2f);
            Vector3 scanCenter = scanFrom.position;

            Collider[] hitColliders = Physics.OverlapBox(scanCenter, halfExtents, Quaternion.identity, layer);

            foreach (Collider hitCollider in hitColliders)
            {
                if (!hitCollider.CompareTag("Enemy")) continue;

                Vector3 directionToTarget = hitCollider.transform.position - scanCenter;
                float distanceSquared = directionToTarget.sqrMagnitude;

                float dotProduct = Vector3.Dot(scanFrom.forward, directionToTarget.normalized);
                if (dotProduct > Mathf.Cos(22.5f * Mathf.Deg2Rad)) // 45 / 2 = 22.5
                {
                    if (distanceSquared < closestDistanceSquared)
                    {
                        closestDistanceSquared = distanceSquared;
                        closestEnemy = hitCollider.gameObject;
                    }
                }
            }

            return closestEnemy;
        }
    }
}
