using Game.Scripts.UI.Menus.Interfaces;
using R3;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public interface IGameUIModel : IUIModel
    {
        public ReactiveProperty<float> PlayerHealth { get; }
        public void MenuButtonClicked();
        public ReactiveProperty<float> PlayerInitialHealth { get; }
        public ReadOnlyReactiveProperty<int> KillCount { get; }
        public ReadOnlyReactiveProperty<int> KillToWin { get; }
        public ReadOnlyReactiveProperty<int> EnemiesCount { get; }
        public ReadOnlyReactiveProperty<float> CurrentExp { get; }
        public ReadOnlyReactiveProperty<float> ExpToNextLevel { get; }
        public ReadOnlyReactiveProperty<int> PlayerLevel { get; }
    }
}
