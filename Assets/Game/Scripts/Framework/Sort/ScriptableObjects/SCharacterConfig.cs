using UnityEngine;

namespace Game.Scripts.Framework.Sort.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "CharacterConfiguration",
        menuName = SOPathConst.ConfigPath + "Character Configuration",
        order = 100)]
    public class SCharacterConfig : ScriptableObject
    {
        [Range(0.1f, 100f)] public float moveSpeed = 5f;

        [Range(45f, 270f)] public float rotationSpeed = 180f;
    }
}
