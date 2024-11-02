using Game.Scripts.Framework.Constants;
using Game.Scripts.Framework.Managers.Settings;
using UnityEngine;

namespace Game.Scripts.Framework.Configuration.SO.Weapon
{
    [CreateAssetMenu(
        fileName = "WeaponSettings",
        menuName = SOPathConst.WeaponPath + "New Weapon",
        order = 100)]
    public class WeaponSettings : SettingsBase
    {
        public override string Description => "Weapon settings";
        public float projectileDamage = 10f;
        public int attackDelayMS = 500;
        public float attackRange = 5f;
        public float projectileSpeed = 100f;
    }
}
