﻿using System;
using System.Collections.Generic;
using Game.Scripts.UI.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Base
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class UIViewBase : MonoBehaviour, IUIView, IDisposable
    {
        protected VisualElement RootVisualElement;
        protected readonly Dictionary<Button, EventCallback<ClickEvent>> CallbacksCache = new();

        public void Awake()
        {
            Debug.LogWarning($"AWAKE {name}");
            var document = gameObject.GetComponent<UIDocument>();

            RootVisualElement = document.visualTreeAsset != null
                ? document.rootVisualElement
                : throw new NullReferenceException("VisualTreeAsset is not set to " + name + " prefab!");

            InitElements();
            InitCallbacksCache();
        }

        public void Show()
        {
            RegisterCallbacks();
            RootVisualElement.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            RootVisualElement.style.display = DisplayStyle.None;
            UnregisterCallbacks();
        }

        /// <summary>
        /// Find visual elements
        /// </summary>
        protected abstract void InitElements();

        /// <summary>
        /// Add to callbacks cache button and callback
        /// </summary>
        protected abstract void InitCallbacksCache();

        private void RegisterCallbacks()
        {
            foreach (var (button, callback) in CallbacksCache) button.RegisterCallback(callback);
        }

        private void UnregisterCallbacks()
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
    }
}