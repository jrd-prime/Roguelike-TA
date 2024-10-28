using Game.Scripts.Framework.Scopes;
using R3;
using UnityEngine;

namespace Game.Scripts.Framework.Managers.Experience
{
    public class ExperienceManager : MonoBehaviour, IExperienceManager
    {
        [SerializeField] private int experienceToLevelUp;

        public ReactiveProperty<int> Experience { get; } = new(0);
        public ReactiveProperty<int> Level { get; } = new(1);

        private int _currentExperience;
        private int _currentLevel;

        public void AddExperience(int experience)
        {
            _currentExperience += experience;
            Experience.Value = _currentExperience;

            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            if (_currentExperience < experienceToLevelUp) return;
            _currentExperience = 0;
            OnLevelUp();
        }

        private void OnLevelUp()
        {
            _currentLevel++;
            Level.Value = _currentLevel;
        }
    }
}
