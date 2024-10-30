using UnityEngine;
using UnityEngine.UIElements;

public interface IJoystickViewModel : IMovementControlViewModel
{
    public void SetScreenOffset(Vector3 offset);
    public void SetJoystickVisual(VisualElement handle, VisualElement ring);
}
