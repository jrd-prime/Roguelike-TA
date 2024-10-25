using System;
using System.Collections.Generic;
using System.Threading;
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
        [SerializeField] private UIViewBase win;
        [SerializeField] private PopUpView popUp;

        private readonly Dictionary<StateType, UIViewBase> _views = new();
        private CancellationTokenSource _cts;

        private void Awake()
        {
            InitializeView(StateType.Menu, menu);
            InitializeView(StateType.Game, game);
            InitializeView(StateType.Pause, pause);
            InitializeView(StateType.GameOver, gameOver);
            InitializeView(StateType.Settings, settings);
            InitializeView(StateType.Win, win);
        }

        public void ShowView(StateType mainMenu) => _views[mainMenu].Show();

        public void HideView(StateType mainMenu) => _views[mainMenu].Hide();


        private void InitializeView(StateType type, UIViewBase uiView)
        {
            if (uiView == null) throw new NullReferenceException($"View of type {type} not set to UIManager prefab!");
            uiView.Hide();
            _views.Add(type, uiView);
        }

        public async void ShowPopUpAsync(string text, int duration = 3000)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                popUp.Hide();
            }

            _cts = new CancellationTokenSource();
            popUp.Show(text);

            try
            {
                await UniTask.Delay(duration, cancellationToken: _cts.Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            popUp.Hide();
            _cts = null;
        }
    }
}
