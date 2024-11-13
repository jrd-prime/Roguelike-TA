using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Base
{
    public abstract class UIToolkitView : UIViewBase
    {
        [SerializeField] protected VisualTreeAsset ViewVisualTreeAsset;
        protected VisualElement RootVisualElement;
        private TemplateContainer _templateContainer;

        public void Awake()
        {
            Debug.LogWarning("awake view " + name);

            if (ViewVisualTreeAsset == null) throw new NullReferenceException("ViewVisualTreeAsset is null.");
            _templateContainer = ViewVisualTreeAsset.Instantiate();

            RootVisualElement = _templateContainer;

            var document = gameObject.GetComponent<UIDocument>();

            // RootVisualElement = document.visualTreeAsset != null
            //     ? document.rootVisualElement
            //     : throw new NullReferenceException("VisualTreeAsset is not set to " + name + " prefab!");

            InitElements();
            Init();
            InitCallbacksCache();
        }

        public override TemplateContainer GetView()
        {
            RegisterCallbacks();
            return _templateContainer;
        }

        public override void Show()
        {
            RegisterCallbacks();
            Debug.LogWarning("show view " + name);
            // RootVisualElement.style.display = DisplayStyle.Flex;
        }

        public override void Hide()
        {
            // RootVisualElement.style.display = DisplayStyle.None;
            Debug.LogWarning("hide view " + name);
            UnregisterCallbacks();
        }
    }
}
