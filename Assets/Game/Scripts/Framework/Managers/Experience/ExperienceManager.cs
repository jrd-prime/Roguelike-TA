using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Managers.Experience
{
    public class ExperienceManager : MonoBehaviour, IExperienceManager
    {
        // TODO: move to config
        [SerializeField] private int baseExpToLevelUp = 100;
        [SerializeField] private float multiplierPerLevel = 1.1f;

        public ReactiveProperty<int> Level { get; } = new(1);
        public ReactiveProperty<int> Experience { get; } = new(0);
        public ReactiveProperty<int> ExperienceToNextLevel { get; } = new();

        private void Awake()
        {
            if (baseExpToLevelUp <= 0) baseExpToLevelUp = 100;
            ExperienceToNextLevel.Value = baseExpToLevelUp;
        }


        public void AddExperience(int experience)
        {
            if (experience <= 0) return;

            if (Experience.CurrentValue + experience >= ExperienceToNextLevel.CurrentValue)
            {
                OnLevelUp(experience);
                return;
            }

            Experience.Value += experience;
        }

        private void OnLevelUp(int experience)
        {
            var expToNextLevel = ExperienceToNextLevel.CurrentValue;
            Level.Value += 1;
            ExperienceToNextLevel.Value = (int)(expToNextLevel * multiplierPerLevel);
            Experience.Value = Experience.CurrentValue + experience - expToNextLevel;
            ForceNotifyAll();
        }

        private void ForceNotifyAll()
        {
            Experience.ForceNotify();
            Level.ForceNotify();
            ExperienceToNextLevel.ForceNotify();
        }

        public void ResetExperience()
        {
            Experience.Value = 0;
            Level.Value = 1;
            ExperienceToNextLevel.Value = baseExpToLevelUp;

            ForceNotifyAll();
        }
    }
}
