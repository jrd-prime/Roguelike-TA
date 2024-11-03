using UnityEngine;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemySkin : MonoBehaviour, IEnemySkin
    {
    }

    public interface IEnemySkin
    {
    }
}
