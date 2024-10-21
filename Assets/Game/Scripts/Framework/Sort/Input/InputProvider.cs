using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Manager.Input
{
    public class InputProvider : MonoBehaviour
    {
        public IInput CurrentInput { get; private set; }
        
        private void Awake()
        {
            var go = gameObject.AddComponent<DesktopInput>();
            CurrentInput = go;

            Debug.LogWarning("InputProvider.Awake " + CurrentInput);
        }
    }
}