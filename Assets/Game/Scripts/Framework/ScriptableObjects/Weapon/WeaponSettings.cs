using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Scripts.Framework.ScriptableObjects.Weapon
{
    [CreateAssetMenu(
        fileName = "WeaponSettings",
        menuName = SOPathConst.WeaponPath + "New Weapon",
        order = 100)]
    public class WeaponSettings : ScriptableObject
    {
        [Title("References")] public AssetReferenceGameObject weaponPrefabReference;
        public AssetReferenceGameObject projectilePrefabReference;

        [Title("Settings")] public float damage = 10f;
        public float attackDelay = 1f;
        public float attackRange = 5f;
    }
}
