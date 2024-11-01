using Game.Scripts.Framework.Constants;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Framework.Configuration.SO
{
    [CreateAssetMenu(
        fileName = "MovementControlSettings",
        menuName = SOPathConst.ConfigPath + "Movement Control Settings",
        order = 100)]
    public class MovementControlSettings : ScriptableObject
    {
        [FormerlySerializedAs("radiusForFullSpeed")] public float offsetForFullSpeed = 100;
    }
}
