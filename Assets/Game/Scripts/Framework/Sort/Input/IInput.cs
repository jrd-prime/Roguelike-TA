using R3;

namespace BackwoodsLife.Scripts.Framework.Manager.Input
{
    
    public interface IInput 
    {
        // public event Action onLMousePress;
        // public event Action<Vector2> onLMouseDrag;
        // public event Action onLMouseRelease;
        // public event Action onLMouseTaped;
        // public event Action<Vector3> OnSingleClick;
        public ReactiveProperty<TouchData> TouchWithData { get; }

        public void ChangeActionMap(string actionMapName);
    }
}
