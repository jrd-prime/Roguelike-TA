﻿using Game.Scripts.Framework.Configuration.SO.Weapon;
using Game.Scripts.Framework.Constants;
using Game.Scripts.Framework.Managers.Settings;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Scripts.Framework.Configuration.SO.Character
{
    [CreateAssetMenu(
        fileName = "CharacterConfiguration",
        menuName = SOPathConst.ConfigPath + "Character Configuration",
        order = 100)]
    public class CharacterSettings : SettingsBase
    {
        public override string Description => "Character settings";

        [Range(0.1f, 100f)] public float moveSpeed = 5f;

        [Range(45f, 270f)] public float rotationSpeed = 180f;
        [Range(30f, 1000f)] public int health = 30;

        public WeaponSettings weapon;

        private void OnValidate()
        {
            Assert.IsNotNull(weapon, "Weapon config is null. Add to auto inject.");
        }
    }
}
