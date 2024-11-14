using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.UI.Base;
using Game.Scripts.UI.Gameplay;
using UnityEngine;

namespace Game.Scripts.UI
{
    // TODO : consider safe areas for the user interface
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<ToolkitView> toolkitViews;

        [SerializeField] private UIToolkitViewer toolkitViewer;

        // [SerializeField] private UIViewBase menu;
        // [SerializeField] private UIViewBase game;
        // [SerializeField] private UIViewBase pause;
        // [SerializeField] private UIViewBase gameOver;
        // [SerializeField] private UIViewBase settings;
        // [SerializeField] private UIViewBase win;
        [SerializeField] private PopUpView popUp;

        // [SerializeField] private GameplayUIView gameplayUIView;

        private readonly Dictionary<StateType, UIViewBase> _views = new();
        private CancellationTokenSource _cts;

        private void Awake()
        {
            var viewsCount = toolkitViews.Count;
            var typesCount = Enum.GetNames(typeof(StateType)).Length;
            if (viewsCount != typesCount)
                Debug.LogWarning($"Not all views are set! /// Set: {viewsCount} / Total: {typesCount}");

            if (toolkitViewer == null) throw new NullReferenceException("ToolkitViewer is null.");

            // throw new Exception(
            //     $"Not all views are set! /// Set: {viewsCount} / Total: {Enum.GetNames(typeof(StateType)).Length}");
            // InitializeView(StateType.Menu, menu);
            // // InitializeView(StateType.Game, game);
            // InitializeView(StateType.Game, gameplayUIView);
            // InitializeView(StateType.Pause, pause);
            // InitializeView(StateType.GameOver, gameOver);
            // InitializeView(StateType.Settings, settings);
            // InitializeView(StateType.Win, win);

            foreach (var toolkitView in toolkitViews) InitializeView(toolkitView.viewForState, toolkitView.view);
        }

        public void ShowView(StateType menuForStateType)
        {
            var toolkitView = _views[menuForStateType];
            toolkitViewer.ShowView(toolkitView.GetView(), toolkitView.SortOrder);
        }

        public void HideView(StateType menuForStateType)
        {
            var toolkitView = _views[menuForStateType];
            toolkitView.Unregister();
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

        private void InitializeView(StateType type, UIViewBase uiView)
        {
            if (uiView == null) throw new NullReferenceException($"View of type {type} not set to UIManager prefab!");
            // uiView.Hide();
            _views.Add(type, uiView);
        }
    }

    [Serializable]
    public struct ToolkitView
    {
        public StateType viewForState;
        public UIToolkitView view;
    }
}
