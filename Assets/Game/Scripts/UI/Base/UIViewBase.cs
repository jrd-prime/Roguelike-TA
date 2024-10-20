using System;
using Game.Scripts.UI.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Base
{
    [RequireComponent(typeof(UIDocument))]
    public class UIViewBase : MonoBehaviour, IUIView
    {
        protected VisualElement RootVisualElement;

        private void Awake()
        {
            var document = gameObject.GetComponent<UIDocument>();

            RootVisualElement = document.visualTreeAsset != null
                ? document.rootVisualElement
                : throw new NullReferenceException("VisualTreeAsset is not set to " + name + " prefab!");
        }

        public void Show()
        {
            Debug.LogWarning($"show {name}");
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            Debug.LogWarning($"hide {name}");
            gameObject.SetActive(false);
        }
    }
}
