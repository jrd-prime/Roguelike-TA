using Game.Scripts.Enemy;
using Game.Scripts.Player;
using Game.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Framework.GameStateMachine
{
    public class GameManager : MonoBehaviour
    {
        [ReadOnly] public bool isGameStarted;
        private IObjectResolver _resolver;
        private EnemiesManager _enemiesManager;
        private PlayerModel _playerModel;
        private UIManager _uiManager;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
            _enemiesManager = _resolver.Resolve<EnemiesManager>();
            _playerModel = _resolver.Resolve<PlayerModel>();
            _uiManager = _resolver.Resolve<UIManager>();
        }
    }
}
