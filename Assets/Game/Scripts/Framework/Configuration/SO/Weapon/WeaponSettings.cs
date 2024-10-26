using Game.Scripts.Framework.Constants;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Framework.Configuration.SO.Weapon
{
    [CreateAssetMenu(
        fileName = "WeaponSettings",
        menuName = SOPathConst.WeaponPath + "New Weapon",
        order = 100)]
    public class WeaponSettings : ScriptableObject
    {
        [FormerlySerializedAs("damage")] [Title("Settings")] public float projectileDamage = 10f;
        public int attackDelayMS = 500;
        public float attackRange = 5f;
        public float projectileSpeed = 100f;
    }
}
