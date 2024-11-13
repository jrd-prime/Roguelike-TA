using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Base
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class UIToolkitView : UIViewBase
    {
        protected VisualElement RootVisualElement;

        public void Awake()
        {
            var document = gameObject.GetComponent<UIDocument>();

            RootVisualElement = document.visualTreeAsset != null
                ? document.rootVisualElement
                : throw new NullReferenceException("VisualTreeAsset is not set to " + name + " prefab!");

            InitElements();
            Init();
            InitCallbacksCache();
        }

        public override void Show()
        {
            RegisterCallbacks();
            RootVisualElement.style.display = DisplayStyle.Flex;
            // gameObject.SetActive(true);
        }

        public override void Hide()
        {
            RootVisualElement.style.display = DisplayStyle.None;
            // gameObject.SetActive(false);
            UnregisterCallbacks();
        }
    }
}
