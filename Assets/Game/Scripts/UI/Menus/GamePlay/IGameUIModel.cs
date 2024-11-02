using Game.Scripts.UI.Menus.Interfaces;
using R3;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public interface IGameUIModel : IUIModel
    {
        // Player
        public ReadOnlyReactiveProperty<int> PlayerHealth { get; }
        public ReadOnlyReactiveProperty<int> PlayerInitialHealth { get; }

        // Enemies
        public ReadOnlyReactiveProperty<int> KillCount { get; }
        public ReadOnlyReactiveProperty<int> KillToWin { get; }
        public ReadOnlyReactiveProperty<int> EnemiesCount { get; }

        // Experience
        public ReadOnlyReactiveProperty<int> Experience { get; }
        public ReadOnlyReactiveProperty<int> ExperienceToNextLevel { get; }
        public ReadOnlyReactiveProperty<int> Level { get; }


        public void MenuButtonClicked();
    }
}
