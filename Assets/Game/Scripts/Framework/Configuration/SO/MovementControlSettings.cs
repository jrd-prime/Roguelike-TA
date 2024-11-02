using Game.Scripts.Framework.Constants;
using Game.Scripts.Framework.Managers.Settings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Framework.Configuration.SO
{
    [CreateAssetMenu(
        fileName = "MovementControlSettings",
        menuName = SOPathConst.ConfigPath + "Movement Control Settings",
        order = 100)]
    public class MovementControlSettings : SettingsBase
    {
        public override string Description => "Movement Control Settings";
        public float offsetForFullSpeed = 100;
    }
}
