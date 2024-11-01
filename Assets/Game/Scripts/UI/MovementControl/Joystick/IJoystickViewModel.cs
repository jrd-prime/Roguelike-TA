using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.MovementControl.Joystick
{
    public interface IJoystickViewModel : IMovementControlViewModel
    {
        public void SetScreenOffset(Vector3 offset);
        public void SetJoystickVisual(VisualElement handle, VisualElement ring);
    }
}
