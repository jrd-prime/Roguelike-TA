﻿using System;
using System.Collections.Generic;
using Game.Scripts.Framework.Bootstrap;
using Game.Scripts.Framework.Configuration.SO;
using UnityEngine;

namespace Game.Scripts.Framework.Managers.Settings
{
    public interface ISettingsManager : ILoadingOperation
    {
        public Dictionary<Type, object> ConfigsCache { get; }
        public T GetConfig<T>() where T : SettingsBase;
    }
}
