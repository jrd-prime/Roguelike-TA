using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Framework.Managers.Altars
{
    public class AltarManager : MonoBehaviour
    {
        [SerializeField] private List<AltarSpawnPoint> _spawnPoints = new(); // <Altar>
    }
}
