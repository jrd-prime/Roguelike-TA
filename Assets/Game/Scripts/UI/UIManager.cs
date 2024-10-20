using System;
using System.Collections.Generic;
using Game.Scripts.UI.Base;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer.Unity;

namespace Game.Scripts.UI
{
    public class UIManager : MonoBehaviour, IInitializable
    {
        // add views prefabs and add to autoinject to ui context container
        [FormerlySerializedAs("menuUI")] [SerializeField] private UIViewBase menu;
        [FormerlySerializedAs("gameUI")] [SerializeField] private UIViewBase game;
        [FormerlySerializedAs("pauseUI")] [SerializeField] private UIViewBase pause;
        [FormerlySerializedAs("gameOverUI")] [SerializeField] private UIViewBase gameOver;
        [FormerlySerializedAs("settingsUI")] [SerializeField] private UIViewBase settings;

        private readonly Dictionary<UIType, UIViewBase> _views = new();

        public void Initialize()
        {
            Debug.LogWarning("UIManager init");
            InitializeView(UIType.Menu, menu);
            InitializeView(UIType.Game, game);
            InitializeView(UIType.Pause, pause);
            InitializeView(UIType.GameOver, gameOver);
            InitializeView(UIType.Settings, settings);
        }

        public void ShowView(UIType mainMenu) => _views[mainMenu].Show();
        public void HideView(UIType mainMenu) => _views[mainMenu].Hide();

        private void InitializeView(UIType type, UIViewBase uiView)
        {
            if (uiView == null) throw new NullReferenceException($"View of type {type} not set to UIManager prefab!");
            uiView.Hide();
            _views.Add(type, uiView);
        }

        public List<UIViewBase> GetAllViews() => new(_views.Values);
    }
}
