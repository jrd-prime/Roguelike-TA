using Game.Scripts.Framework.Helpers;
using Game.Scripts.UI.Base;
using Game.Scripts.UI.MovementControl.FullScreen;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.GamePlay
{
    public class GameUIModel : UIModelBase, IGameUIModel
    {
        public ReadOnlyReactiveProperty<int> PlayerHealth => GameManager.PlayerHealth;
        public ReadOnlyReactiveProperty<int> PlayerInitialHealth => GameManager.PlayerInitialHealth;
        public ReadOnlyReactiveProperty<int> KillCount => GameManager.KillCount;
        public ReadOnlyReactiveProperty<int> KillToWin => GameManager.KillToWin;
        public ReadOnlyReactiveProperty<int> EnemiesCount => GameManager.EnemiesCount;

        public ReadOnlyReactiveProperty<int> Experience => GameManager.Experience;
        public ReadOnlyReactiveProperty<int> ExperienceToNextLevel => GameManager.ExperienceToNextLevel;
        public ReadOnlyReactiveProperty<int> Level => GameManager.Level;


        public void MenuButtonClicked() => StateMachine.ChangeStateTo(StateType.Pause);

        public ReactiveProperty<bool> IsTouchPositionVisible => _movementModel.IsTouchPositionVisible;
        public ReactiveProperty<Vector2> RingPosition => _movementModel.RingPosition;
        public ReadOnlyReactiveProperty<bool> IsGameStarted => GameManager.IsGameStarted;

        private IFullScreenMovementModel _movementModel;

        public override void Initialize()
        {
            _movementModel = ResolverHelp.ResolveAndCheck<IFullScreenMovementModel>(Container);
        }

        public void OnDownEvent(PointerDownEvent evt) => _movementModel.OnDownEvent(evt);
        public void OnMoveEvent(PointerMoveEvent evt) => _movementModel.OnMoveEvent(evt);
        public void OnUpEvent(PointerUpEvent _) => _movementModel.OnUpEvent(_);
        public void OnOutEvent(PointerOutEvent _) => _movementModel.OnOutEvent(_);
    }
}
