using UnityEngine;

namespace Game.Scripts.Framework.Configuration.SO
{
    public abstract class SettingsBase : ScriptableObject, ISettings
    {
        public abstract string Description { get; }
    }
}
