using System;
using System.Collections.Generic;
using Game.Scripts.UI.Menus.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Base
{
    public abstract class UIViewBase : MonoBehaviour, IUIView, IDisposable
    {
        [SerializeField] public int SortOrder;
        protected readonly Dictionary<Button, EventCallback<ClickEvent>> CallbacksCache = new();
        protected readonly CompositeDisposable Disposables = new();


        public abstract void Show();
        public abstract void Hide();

        protected abstract void Init();

        /// <summary>
        /// Find visual elements
        /// </summary>
        protected abstract void InitElements();

        /// <summary>
        /// Add to callbacks cache button and callback
        /// </summary>
        protected abstract void InitCallbacksCache();

        protected void RegisterCallbacks()
        {
            foreach (var (button, callback) in CallbacksCache) button.RegisterCallback(callback);
        }

        protected void UnregisterCallbacks()
        {
            foreach (var (button, callback) in CallbacksCache) button.UnregisterCallback(callback);
        }

        protected static void CheckOnNull(VisualElement element, string elementIDName, string className)
        {
            Assert.IsNotNull(element, $"{elementIDName} in {className} is null");
        }

        public void Dispose()
        {
            UnregisterCallbacks();
        }

        public abstract TemplateContainer GetView();

        public void Unregister()
        {
            Debug.LogWarning("unregister view callback " + name);
            UnregisterCallbacks();
        }
    }
}
