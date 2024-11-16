using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Framework.Configuration.SO.Enemy;

namespace Game.Scripts.Framework.Helpers
{
    public static class SettingsExtension
    {
        public static EnemySettings GetRandomSettings(this List<EnemySettings> settings)
        {
            if (settings == null)
                throw new ArgumentException("The settings collection cannot be null.");
            if (settings.Count == 0)
                throw new ArgumentException("The settings collection cannot be empty.");

            var random = new Random();

            var roll = random.Next(0, 100);

            var selectedType = roll switch
            {
                < 80 => EnemySpawnChance.Common,
                < 95 => EnemySpawnChance.Rare,
                _ => EnemySpawnChance.Legendary
            };

            var selectedEnemies = settings.Where(e => e.spawnChance == selectedType).ToList();
            if (selectedEnemies.Count == 0)
                throw new InvalidOperationException($"No enemies found for type {selectedType}");


            var randomIndex = random.Next(0, selectedEnemies.Count);
            return selectedEnemies[randomIndex];
        }
    }
}
