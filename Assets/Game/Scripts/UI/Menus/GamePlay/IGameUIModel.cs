using Game.Scripts.UI.Menus.Interfaces;
using R3;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public interface IGameUIModel : IUIModel
    {
        public ReactiveProperty<int> PlayerHealth { get; }
        public ReactiveProperty<int> PlayerInitialHealth { get; }
        public ReadOnlyReactiveProperty<int> KillCount { get; }
        public ReadOnlyReactiveProperty<int> KillToWin { get; }
        public ReadOnlyReactiveProperty<int> EnemiesCount { get; }
        public ReadOnlyReactiveProperty<int> CurrentExp { get; }
        public ReadOnlyReactiveProperty<int> ExpToNextLevel { get; }
        public ReadOnlyReactiveProperty<int> PlayerLevel { get; }
        public void MenuButtonClicked();
    }
}
