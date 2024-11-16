using R3;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.GamePlay.Components
{
    public sealed class Kills
    {
        private Label _killCountLabel;
        private int _killCount;
        private int _killToWin;

        private readonly VisualElement _root;
        private readonly GameUIViewModel _viewModel;
        private readonly CompositeDisposable _disposables;

        public Kills(in GameUIViewModel viewModel, in VisualElement root, in CompositeDisposable disposables)
        {
            _viewModel = viewModel;
            _root = root;
            _disposables = disposables;
        }

        public void InitElements()
        {
            _killCountLabel = _root.Q<Label>(UIConst.KillCountLabelIDName);
        }

        public void Init()
        {
            _viewModel.KillCount
                .Subscribe(killCount =>
                {
                    _killCount = killCount;
                    UpdateKillCountLabel();
                })
                .AddTo(_disposables);

            _viewModel.KillToWin
                .Subscribe(killToWin =>
                {
                    _killToWin = killToWin;
                    UpdateKillCountLabel();
                })
                .AddTo(_disposables);
        }

        private void UpdateKillCountLabel() => _killCountLabel.text = $"{_killCount} / {_killToWin}";
    }
}
