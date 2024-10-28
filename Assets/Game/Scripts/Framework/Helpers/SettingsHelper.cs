using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Framework.Helpers
{
    public static class SettingsHelper
    {
        public static T GetRandomSettings<T>(this IEnumerable<T> settings) where T : ScriptableObject
        {
            if (settings == null)
                throw new ArgumentException("The settings collection cannot be null.");

            var list = settings.ToList();

            if (list.Count == 0)
                throw new ArgumentException("The settings collection cannot be empty.");

            return list[Random.Range(0, list.Count)];
        }
    }
}
