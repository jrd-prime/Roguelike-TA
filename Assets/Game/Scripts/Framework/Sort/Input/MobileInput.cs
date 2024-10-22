using UnityEngine;

namespace Game.Scripts.Framework.Sort.Input
{
    public sealed class MobileInput : MonoBehaviour
    {
        private JInputActions _gameInputActions;

        private void Awake()
        {
            _gameInputActions = new JInputActions();
            _gameInputActions.Enable();
        }
    }
}
