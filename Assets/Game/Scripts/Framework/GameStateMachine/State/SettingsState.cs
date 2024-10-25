using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Framework.GameStateMachine.State
{
    public class SettingsState : GameStateBase, IGameState
    {
        public void Enter()
        {
            Debug.LogWarning("s ettings state enter");
            GameManager.ShowView(UIType.Settings);
        }

        public void Exit()
        {
            Debug.LogWarning("settings state exit");
            GameManager.HideView(UIType.Settings);
        }
    }
}
