using Game.Scripts.Framework.CommonModel;
using Game.Scripts.Framework.Configuration.SO.Character;
using R3;
using UnityEngine;

namespace Game.Scripts.Player.Interfaces
{
    public interface IPlayerModel : IMovableModel, ITrackableModel
    {
        public ReactiveProperty<int> Health { get; }
        public CharacterSettings CharSettings { get; }
        public ReactiveProperty<bool> IsShooting { get; }
        public void SetHealth(int health);
        public void TakeDamage(int damage);
        public void ResetPlayer();
        public void ShootToTargetAsync(GameObject nearestEnemy);
        public ReactiveProperty<bool> IsGameStarted { get; }
        public void SetGameStarted(bool value);
    }
}
