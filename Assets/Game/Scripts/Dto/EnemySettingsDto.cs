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
        public float AttackDelayInSec;
        public int Damage;
        public int Health;
        public int Experience;
    }
}
