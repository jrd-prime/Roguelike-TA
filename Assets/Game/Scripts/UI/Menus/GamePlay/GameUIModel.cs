using Game.Scripts.UI.Base;
using R3;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public class GameUIModel : UIModelBase, IGameUIModel
    {
        public ReadOnlyReactiveProperty<int> PlayerHealth => GameManager.PlayerHealth;
        public ReadOnlyReactiveProperty<int> PlayerInitialHealth => GameManager.PlayerInitialHealth;
        public ReadOnlyReactiveProperty<int> KillCount => GameManager.KillCount;
        public ReadOnlyReactiveProperty<int> KillToWin => GameManager.KillToWin;
        public ReadOnlyReactiveProperty<int> EnemiesCount => GameManager.EnemiesCount;

        public ReadOnlyReactiveProperty<int> Experience => GameManager.Experience;
        public ReadOnlyReactiveProperty<int> ExperienceToNextLevel => GameManager.ExperienceToNextLevel;
        public ReadOnlyReactiveProperty<int> Level => GameManager.Level;


        public void MenuButtonClicked() => StateMachine.ChangeStateTo(StateType.Pause);

        public override void Initialize()
        {
        }
    }
}
