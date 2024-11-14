using Game.Scripts.Framework.Constants;
using UnityEngine;

namespace Game.Scripts.Framework.Configuration.SO
{
    [CreateAssetMenu(
        fileName = "GameSettings",
        menuName = SOPathConst.ConfigPath + "Game Settings",
        order = 100)]
    public class GameSettings : SettingsBase
    {
        public override string Description => "Game settings";

        public int spawnDelay = 500;
        public int minEnemiesOnMap = 5;
        public int maxEnemiesOnMap = 30;
        public int killsToWin = 999;
    }
}
