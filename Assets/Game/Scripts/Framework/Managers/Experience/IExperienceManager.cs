using Game.Scripts.Framework.Managers.Game;
using R3;

namespace Game.Scripts.Framework.Managers.Experience
{
    public interface IExperienceManager
    {
        public ReactiveProperty<int> CurrentExp { get; }
        public ReactiveProperty<int> ExpToNextLevel { get; }
        public ReactiveProperty<int> Level { get; }
        public void AddExperience(int experience);
    }
}
