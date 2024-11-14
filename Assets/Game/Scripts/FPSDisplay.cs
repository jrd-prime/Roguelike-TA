using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class FPSDisplay : MonoBehaviour
    {
        public TMP_Text fpsText;
        private float deltaTime;

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = $"FPS: {fps:0.}";
        }
    }
}
