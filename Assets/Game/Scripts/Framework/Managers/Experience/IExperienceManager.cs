using R3;

namespace Game.Scripts.Framework.Scopes
{
    public interface IExperienceManager
    {
        public ReactiveProperty<int> Experience { get; }
        public ReactiveProperty<int> Level { get; }
        public void AddExperience(int experience);
    }
}
