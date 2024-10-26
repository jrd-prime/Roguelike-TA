using UnityEngine;

namespace Game.Scripts.Framework.Systems
{
    public static class FindTargetSystem
    {
        public static GameObject FindNearestInBox(Transform scanFrom, float horizontal, float vertical, LayerMask layer)
        {
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;
            Vector3 halfExtents = new Vector3(horizontal / 2f, 1f, vertical / 2f);
            Vector3 scanCenter = scanFrom.position;


            Collider[] hitColliders =
                Physics.OverlapBox(scanCenter, halfExtents, Quaternion.identity, layer);

            foreach (Collider hitCollider in hitColliders)
            {
                if (!hitCollider.CompareTag("Enemy")) continue;

                Vector3 directionToTarget = (hitCollider.transform.position - scanCenter).normalized;

                float angle = Vector3.Angle(scanFrom.forward, directionToTarget);

                if (angle <= 45 / 2f)
                {
                    float distanceToEnemy = Vector3.Distance(scanCenter, hitCollider.transform.position);

                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = hitCollider.gameObject;
                    }
                }
            }

            return closestEnemy;
        }
    }
}
