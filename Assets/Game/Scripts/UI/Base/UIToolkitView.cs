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
            if (ViewVisualTreeAsset == null) throw new NullReferenceException("ViewVisualTreeAsset is null.");
            _templateContainer = ViewVisualTreeAsset.Instantiate();

            RootVisualElement = _templateContainer;

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
