using System;
using System.Collections.Generic;
using Game.Scripts.Framework.GameStateMachine.State;
using UnityEngine;
using VContainer.Unity;

namespace Game.Scripts.UI
{
    public class UIManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private UIView menuUI;
        [SerializeField] private UIView gameUI;
        [SerializeField] private UIView pauseUI;
        [SerializeField] private UIView gameOverUI;
        [SerializeField] private UIView settingsUI;

        private readonly Dictionary<UIType, UIView> _views = new();

        public void Initialize()
        {
            Debug.LogWarning("UIManager init");
            InitializeView(UIType.Menu, menuUI);
            InitializeView(UIType.Game, gameUI);
            InitializeView(UIType.Pause, pauseUI);
            InitializeView(UIType.GameOver, gameOverUI);
            InitializeView(UIType.Settings, settingsUI);
        }

        public void ShowView(UIType mainMenu) => _views[mainMenu].Show();
        public void HideView(UIType mainMenu) => _views[mainMenu].Hide();

        private void InitializeView(UIType type, UIView view)
        {
            if (view == null) throw new NullReferenceException($"View of type {type} not set to UIManager prefab!");
            view.Hide();
            _views.Add(type, view);
        }

        public List<UIView> GetAllViews() => new(_views.Values);
    }
}
