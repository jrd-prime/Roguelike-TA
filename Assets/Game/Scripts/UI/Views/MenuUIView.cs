using System;
using Game.Scripts.UI.Base;
using Game.Scripts.UI.ViewModels;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Views
{
    public class MenuUIView : UIViewCustom<MenuUIViewModel>
    {
        private void Start()
        {
            Debug.LogWarning("MenuView init");
            if (ViewModel == null) throw new NullReferenceException("MenuViewModel is null");
            var startButton = RootVisualElement.Q<Button>("start-btn");

            startButton.text = "Start11";
            startButton.RegisterCallback<ClickEvent>(_ => ViewModel.StartButtonClicked.OnNext(Unit.Default));
            startButton.RegisterCallback<ClickEvent>(_ => Debug.Log("Start button clicked"));
        }
    }
}
