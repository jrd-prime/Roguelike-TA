﻿using R3;

namespace Game.Scripts.Framework.Managers.Enemy
{
    public interface IEnemiesManager
    {
        public ReactiveProperty<int> Kills { get; }
        public ReactiveProperty<int> KillToWin { get; }
        public void StartSpawnEnemiesAsync(int killToWin, int minOnMap, int maxOnMap, int spawnDelay);
        public void StopSpawn();
        public void EnemyDied(string enemyID);
    }
}