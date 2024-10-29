using Game.Scripts.Framework.Managers.Game;
using R3;

namespace Game.Scripts.Framework.Managers.Experience
{
    public interface IExperienceManager
    {
        public ReactiveProperty<float> CurrentExp { get; }
        public ReactiveProperty<float> ExpToNextLevel { get; }
        public ReactiveProperty<int> Level { get; }
        public void AddExperience(float experience);
    }
}
