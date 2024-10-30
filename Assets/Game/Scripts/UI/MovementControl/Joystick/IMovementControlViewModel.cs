using UnityEngine.UIElements;

public interface IMovementControlViewModel
{
    public void OnDownEvent(PointerDownEvent evt);
    public void OnMoveEvent(PointerMoveEvent evt);
    public void OnUpEvent(PointerUpEvent evt);
    public void OnOutEvent(PointerOutEvent _);
}
