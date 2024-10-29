using System;
using Game.Scripts.Framework.Managers.Enemy;
using Game.Scripts.Framework.Managers.Experience;
using Game.Scripts.Player.Interfaces;
using Game.Scripts.UI;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Managers.Game
{
    public class GameManagerBase : MonoBehaviour, IDisposable
    {
        [SerializeField] protected int spawnDelay = 500;
        [SerializeField] protected int minEnemiesOnMap = 5;
        [SerializeField] protected int maxEnemiesOnMap = 10;
        [SerializeField] protected int killsToWin = 100;

        public ReadOnlyReactiveProperty<int> KillCount => EnemiesManager.Kills;
        public ReadOnlyReactiveProperty<int> KillToWin => EnemiesManager.KillToWin;
        public ReadOnlyReactiveProperty<int> EnemiesCount => EnemiesManager.EnemiesCount;

        public ReadOnlyReactiveProperty<int> PlayerLevel => ExperienceManager.Level;
        public ReadOnlyReactiveProperty<float> PlayerExp => ExperienceManager.CurrentExp;
        public ReadOnlyReactiveProperty<float> ExpToNextLevel => ExperienceManager.ExpToNextLevel;

        [ReadOnly] public ReactiveProperty<bool> isGameStarted { get; } = new();
        protected IObjectResolver Resolver;
        protected IEnemiesManager EnemiesManager;
        protected IPlayerModel PlayerModel;
        protected UIManager UIManager;
        protected IExperienceManager ExperienceManager;

        protected bool IsGamePaused;
        protected readonly CompositeDisposable Disposables = new();


        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            Debug.LogWarning("game manager construct");
            Resolver = resolver;
            EnemiesManager = Resolver.Resolve<IEnemiesManager>();
            PlayerModel = Resolver.Resolve<IPlayerModel>();
            UIManager = Resolver.Resolve<UIManager>();
            ExperienceManager = Resolver.Resolve<IExperienceManager>();
        }

        protected void Awake()
        {
            Assert.IsNotNull(PlayerModel, $"PlayerModel is null. {this}");
            Assert.IsNotNull(EnemiesManager, $"EnemiesManager is null. {this}");
            Assert.IsNotNull(UIManager, $"UIManager is null. {this}");
        }

        public void Dispose()
        {
        }
    }
}
