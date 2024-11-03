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
        [Range(0f, 100f)] public float Speed;
        [Range(0f, 10f)] public float AttackDelayInSec;
        [Range(0, 1000)] public int Damage;
        [Range(1, 10000)] public int Health;
        [Range(0, 10000)] public int Experience;
        [Range(0f, 10f)] public float DisappearanceDuration;
        [Range(0f, 20f)] public float AttackDistance;
        [Range(0f, 100f)] public float RotationSpeed;

        public EnemySettingsDto(Animator animator, IPlayerModel followTargetModel, EnemySettings enemySettings)
        {
            if (enemySettings == null) throw new ArgumentNullException(nameof(enemySettings));

            ID = Guid.NewGuid().ToString();
            Animator = animator;
            Target = followTargetModel ?? throw new ArgumentNullException(nameof(followTargetModel));
            Speed = enemySettings.speed;
            AttackDelayInSec = enemySettings.attackDelayInSec;
            Damage = enemySettings.damage;
            Health = enemySettings.health;
            Experience = enemySettings.baseExperiencePoints;
            DisappearanceDuration = enemySettings.disappearanceDuration;
            AttackDistance = enemySettings.attackDistance;
            RotationSpeed = enemySettings.rotationSpeed;
        }
    }
}
