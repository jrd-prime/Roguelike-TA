namespace Game.Scripts.Framework.GameStateMachine
{
    public interface IGameState
    {
        void Enter();
        void Update();
        void Exit();
    }
}
