using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Managers.Experience
{
    public class ExperienceManager : MonoBehaviour, IExperienceManager
    {
        [SerializeField] private float baseExpToLevelUp;
        [SerializeField] private float multiplierPerLevel = 1.1f;

        public ReactiveProperty<float> CurrentExp { get; } = new(0);
        public ReactiveProperty<float> ExpToNextLevel { get; } = new();
        public ReactiveProperty<int> Level { get; } = new(1);

        private void Awake()
        {
            Debug.LogWarning("Experience manager awake");
            if (baseExpToLevelUp <= 0) baseExpToLevelUp = 100f;
            ExpToNextLevel.Value = baseExpToLevelUp;
        }

        public void AddExperience(float experience)
        {
            if (experience <= 0) return;

            if (CurrentExp.CurrentValue + experience >= ExpToNextLevel.CurrentValue)
            {
                OnLevelUp(experience);
                return;
            }

            CurrentExp.Value += experience;
        }

        private void OnLevelUp(float experience)
        {
            CurrentExp.Value = CurrentExp.CurrentValue + experience - ExpToNextLevel.CurrentValue;
            Level.Value += 1;
            ExpToNextLevel.Value *= multiplierPerLevel;
        }
    }
}
