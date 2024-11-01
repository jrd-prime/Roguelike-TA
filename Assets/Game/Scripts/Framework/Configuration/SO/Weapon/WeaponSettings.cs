using Game.Scripts.Framework.Constants;
using UnityEngine;

namespace Game.Scripts.Framework.Configuration.SO.Weapon
{
    [CreateAssetMenu(
        fileName = "WeaponSettings",
        menuName = SOPathConst.WeaponPath + "New Weapon",
        order = 100)]
    public class WeaponSettings : ScriptableObject
    {
        public float projectileDamage = 10f;
        public int attackDelayMS = 500;
        public float attackRange = 5f;
        public float projectileSpeed = 100f;
    }
}
