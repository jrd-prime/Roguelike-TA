using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Framework.Weapon
{
    public class CharacterGun : WeaponBase
    {
        private async void Awake()
        {
            var playerView = GetComponent<PlayerView>();
        }
    }
}
