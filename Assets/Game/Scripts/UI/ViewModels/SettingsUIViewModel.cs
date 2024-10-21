using Game.Scripts.UI.Base;
using Game.Scripts.UI.Models;
using R3;
using UnityEngine;

namespace Game.Scripts.UI.ViewModels
{
    public class SettingsUIViewModel : UIViewModelCustom<ISettingsUIModel>
    {
        public Subject<Unit> MusicButtonClicked { get; } = new();
        public Subject<Unit> VfxButtonClicked { get; } = new();
        public Subject<Unit> MenuButtonClicked { get; } = new();

        public override void Initialize()
        {
            MusicButtonClicked.Subscribe(_ => Model.MusicButtonClicked()).AddTo(Disposables);
            VfxButtonClicked.Subscribe(_ => Model.VfxButtonClicked()).AddTo(Disposables);
            MenuButtonClicked.Subscribe(_ =>
            {
                Model.MenuButtonClicked();
                Debug.LogWarning($"settings menu button clicked");
            }).AddTo(Disposables);
        }
    }
}
