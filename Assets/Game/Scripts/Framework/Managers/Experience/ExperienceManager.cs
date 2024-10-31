using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Managers.Experience
{
    public class ExperienceManager : MonoBehaviour, IExperienceManager
    {
        [SerializeField] private int baseExpToLevelUp = 100;
        [SerializeField] private float multiplierPerLevel = 1.1f;

        public ReactiveProperty<int> CurrentExp { get; } = new(0);
        public ReactiveProperty<int> ExpToNextLevel { get; } = new();
        public ReactiveProperty<int> Level { get; } = new(1);

        private void Awake()
        {
            if (baseExpToLevelUp <= 0) baseExpToLevelUp = 100;
            ExpToNextLevel.Value = baseExpToLevelUp;
        }

        public void AddExperience(int experience)
        {
            if (experience <= 0) return;

            if (CurrentExp.CurrentValue + experience >= ExpToNextLevel.CurrentValue)
            {
                OnLevelUp(experience);
                return;
            }

            CurrentExp.Value += experience;
        }

        private void OnLevelUp(int experience)
        {
            var expToNextLevel = ExpToNextLevel.CurrentValue;
            Level.Value += 1;
            ExpToNextLevel.Value = (int)(expToNextLevel * multiplierPerLevel);
            CurrentExp.Value = CurrentExp.CurrentValue + experience - expToNextLevel;
            ForceNotifyAll();
        }

        private void ForceNotifyAll()
        {
            CurrentExp.ForceNotify();
            Level.ForceNotify();
            ExpToNextLevel.ForceNotify();
        }
    }
}
