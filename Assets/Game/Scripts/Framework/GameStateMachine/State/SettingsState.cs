using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class SettingsState : GameState, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("s ettings state enter");
            UIManager.ShowView(UIType.Settings);
        }

        public void Update()
        {
            Debug.LogWarning("settings state update");
        }

        public void Exit()
        {
            Debug.LogWarning("settings state exit");
            UIManager.HideView(UIType.Settings);
        }
    }
}
