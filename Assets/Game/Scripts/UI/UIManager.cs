using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.UI.Base;
using UnityEngine;

namespace Game.Scripts.UI
{
    // TODO : consider safe areas for the user interface
    public class UIManager : MonoBehaviour //, IInitializable
    {
        [SerializeField] private UIViewBase menu;
        [SerializeField] private UIViewBase game;
        [SerializeField] private UIViewBase pause;
        [SerializeField] private UIViewBase gameOver;
        [SerializeField] private UIViewBase settings;
        [SerializeField] private PopUpView popUp;

        private readonly Dictionary<UIType, UIViewBase> _views = new();

        private void Awake()
        {
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

        public async void ShowPopUpAsync(string text, int duration = 600)
        {
            popUp.Show(text);
            await UniTask.Delay(duration);
            popUp.Hide();
        }
    }
}
