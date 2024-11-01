using UnityEngine.UIElements;

namespace Game.Scripts.UI.MovementControl.Joystick
{
    public interface IJoystickModel : IMovementControlModel
    {
        public void SetJoystickHandle(VisualElement joystickHandle);
        public void SetJoystickRing(VisualElement joystickRing);
        public VisualElement JoystickHandle { get; }
        public VisualElement JoystickRing { get; }
    }
}
