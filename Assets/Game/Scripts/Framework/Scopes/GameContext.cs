using System;
using Game.Scripts.Enemy;
using Game.Scripts.Framework.GameStateMachine;
using Game.Scripts.Framework.GameStateMachine.State;
using Game.Scripts.Framework.Managers.Enemy;
using Game.Scripts.Framework.Managers.Game;
using Game.Scripts.Framework.Managers.SpawnPoints;
using Game.Scripts.Framework.Managers.Weapon;
using Game.Scripts.Player;
using Game.Scripts.Player.Interfaces;
using Game.Scripts.UI;
using Game.Scripts.UI.Joystick;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Framework.Scopes
{
    public class GameContext : LifetimeScope
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private SpawnPointsManager spawnPointsManager;
        [SerializeField] private EnemiesManager enemiesManager;
        [SerializeField] private WeaponManager weaponManager;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.LogWarning("<color=cyan>GAME CONTEXT</color>");


            builder.RegisterComponent(gameManager).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(weaponManager).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(uiManager).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(spawnPointsManager).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponent(enemiesManager).AsSelf().AsImplementedInterfaces();
            
            // Movement
            builder.Register<JoystickModel>(Lifetime.Singleton).AsSelf();
            builder.Register<JoystickViewModel>(Lifetime.Singleton).AsSelf();

            // Character
            builder.Register<PlayerViewModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerModel>(Lifetime.Singleton).As<IPlayerModel>().As<IInitializable>();


            // State machine
            builder.Register<StateMachine>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameStateBase>(Lifetime.Singleton).AsSelf();
            builder.Register<MenuState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GamePlayState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SettingsState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<GameOverState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PauseState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<WinState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}
