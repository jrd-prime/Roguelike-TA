using System;
using R3;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Game.Scripts.UI.Views
{
    public class MenuView : UIView
    {
        private MenuViewModel _viewModel;

        [Inject]
        private void Construct(MenuViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Start()
        {
            Debug.LogWarning("MenuView init");
            if (_viewModel == null) throw new NullReferenceException("MenuViewModel is null");

            var startButton = RootVisualElement.Q<Button>("start-btn");

            startButton.RegisterCallback<ClickEvent>(_ => _viewModel.OnButtonClickCommand.OnNext(Unit.Default));
        }
    }

    public class MenuViewModel
    {
        private readonly MenuModel model;
        public Subject<Unit> OnButtonClickCommand = new();

        public MenuViewModel(MenuModel model)
        {
            this.model = model;


            // Подписка на выполнение команды
            OnButtonClickCommand.Subscribe(_ => model.ExecuteAction());
        }
    }

    public class MenuModel
    {
        public void ExecuteAction()
        {
            Debug.LogWarning("Action in Model executed");
        }
    }
}
