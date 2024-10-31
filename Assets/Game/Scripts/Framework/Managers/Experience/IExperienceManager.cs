using Game.Scripts.Framework.Managers.Game;
using R3;

namespace Game.Scripts.Framework.Managers.Experience
{
    public interface IExperienceManager
    {
        public ReactiveProperty<int> Level { get; }
        public ReactiveProperty<int> Experience { get; }
        public ReactiveProperty<int> ExperienceToNextLevel { get; }
        public void AddExperience(int experience);
        public void ResetExperience();
    }
}
