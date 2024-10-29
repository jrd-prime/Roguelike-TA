using Game.Scripts.Framework.CommonModel;
using UnityEngine;

namespace Game.Scripts.Dto
{
    public struct EnemySettingsDto
    {
        public string ID;
        public Animator Animator;
        public ITrackableModel Target;
        public float Speed;
        public float AttackDelayMs;
        public float Damage;
        public float Health;
        public float Experience;
    }
}
