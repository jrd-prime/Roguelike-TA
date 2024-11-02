using R3;
using UnityEngine;

namespace Game.Scripts.UI.MovementControl.FullScreen
{
    public interface IFullScreenMovementViewModel : IMovementControlViewModel
    {
        public ReadOnlyReactiveProperty<bool> IsTouchPositionVisible { get; }
        public ReadOnlyReactiveProperty<Vector2> RingPosition { get; }
    }
}
