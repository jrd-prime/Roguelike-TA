using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class FPSDisplay : MonoBehaviour
    {
        public TMP_Text fpsText;
        private float _deltaTime;

        private void Update()
        {
            _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
            var fps = 1.0f / _deltaTime;
            fpsText.text = $"{fps:0.}";
        }
    }
}
