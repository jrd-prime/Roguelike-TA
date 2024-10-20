using System;
using Game.Scripts.UI.Base;
using Game.Scripts.UI.Models;
using R3;

namespace Game.Scripts.UI.ViewModels
{
    public class MenuUIViewModel : UIViewModelCustom<IMenuUIModel>
    {
        public Subject<Unit> StartButtonClicked { get; } = new();
        public Subject<Unit> SettingsButtonClicked { get; } = new();
        public Subject<Unit> ExitButtonClicked { get; } = new();

        private readonly CompositeDisposable _disposables = new();

        public override void Initialize()
        {
            if (Model == null) throw new NullReferenceException($"{typeof(MenuUIModel)} is null");

            StartButtonClicked.Subscribe(_ => Model.StartButtonClicked()).AddTo(_disposables);
            SettingsButtonClicked.Subscribe(_ => Model.SettingsButtonClicked()).AddTo(_disposables);
            ExitButtonClicked.Subscribe(_ => Model.ExitButtonClicked()).AddTo(_disposables);
        }
    }
}
