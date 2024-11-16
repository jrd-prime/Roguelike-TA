using System;
using Game.Scripts.Framework.CommonModel;
using Game.Scripts.Framework.Configuration.SO.Enemy;
using Game.Scripts.Player.Interfaces;
using UnityEngine;

namespace Game.Scripts.Dto
{
    public struct EnemySettingsDto
    {
        public string ID;
        public Animator Animator;
        public ITrackableModel Target;
        public EnemySpawnChance SpawnChance;
        public float Speed;
        public float AttackDelayInSec;
        public int Damage;
        public int Health;
        public int Experience;
        public float DisappearanceDuration;
        public float AttackDistance;
        public float RotationSpeed;

        public EnemySettingsDto(Animator animator, IPlayerModel followTargetModel, EnemySettings enemySettings)
        {
            if (enemySettings == null) throw new ArgumentNullException(nameof(enemySettings));

            ID = Guid.NewGuid().ToString();
            Animator = animator;
            Target = followTargetModel ?? throw new ArgumentNullException(nameof(followTargetModel));
            Speed = enemySettings.speed;
            AttackDelayInSec = enemySettings.AttackDelayInSec;
            Damage = enemySettings.Damage;
            Health = enemySettings.Health;
            Experience = enemySettings.baseExperiencePoints;
            DisappearanceDuration = enemySettings.disappearanceDuration;
            AttackDistance = enemySettings.attackDistance;
            RotationSpeed = enemySettings.rotationSpeed;
            SpawnChance = enemySettings.spawnChance;
        }
    }
}
