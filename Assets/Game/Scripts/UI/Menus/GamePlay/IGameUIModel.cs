using Game.Scripts.UI.Menus.Interfaces;
using R3;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public interface IGameUIModel : IUIModel
    {
        public ReactiveProperty<float> PlayerHealth { get; }
        public void MenuButtonClicked();
        public ReactiveProperty<float> PlayerInitialHealth { get; }
        public ReactiveProperty<int> KillCount { get; }
        public ReactiveProperty<int> KillToWin { get; }
    }
}
