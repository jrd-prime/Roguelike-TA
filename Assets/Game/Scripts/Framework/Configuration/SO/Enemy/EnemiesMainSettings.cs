﻿using System.Collections.Generic;
using Game.Scripts.Framework.Constants;
using UnityEngine;

namespace Game.Scripts.Framework.Configuration.SO.Enemy
{
    [CreateAssetMenu(
        fileName = "EnemiesMainSettings",
        menuName = SOPathConst.ConfigPath + "New Enemies Main Settings",
        order = 100)]
    public class EnemiesMainSettings : ScriptableObject
    {
        public List<EnemySettings> enemies = new(); // <EnemySettings>
    }
}
