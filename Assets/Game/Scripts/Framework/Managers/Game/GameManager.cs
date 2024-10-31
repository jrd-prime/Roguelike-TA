using UnityEngine;

namespace Game.Scripts.Framework.Managers.Game
{
    public class GameManager : GameManagerBase
    {
        public void GameOver()
        {
            EnemiesManager.StopSpawn();
            isGameStarted.Value = false;
        }

        public void StopTheGame()
        {
            EnemiesManager.StopSpawn();
            isGameStarted.Value = false;
        }

        public void StartNewGame()
        {
            if (isGameStarted.CurrentValue) return;

            isGameStarted.Value = true;
            PlayerModel.ResetPlayer();
            ExperienceManager.ResetExperience();

            EnemiesManager.StartSpawnEnemiesAsync(killsToWin, minEnemiesOnMap, maxEnemiesOnMap, spawnDelay);
        }

        public void Pause()
        {
            IsGamePaused = true;
            Time.timeScale = 0;
        }

        public void UnPause()
        {
            IsGamePaused = false;
            Time.timeScale = 1;
        }
    }
}
