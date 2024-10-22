using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Scripts.Framework.Sort.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "MainConfigurations",
        menuName = SOPathConst.ConfigPath + "Main Configurations List",
        order = 100)]
    public class SMainConfig : ScriptableObject
    {
        [Title("Character")] public SCharacterConfig characterConfig;


        private void OnValidate()
        {
            Assert.IsNotNull(characterConfig, "Main Configurations: Character config is null!");
        }
    }
}
