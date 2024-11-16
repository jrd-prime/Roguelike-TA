using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using R3;
using UnityEngine.UIElements;

namespace Game.Scripts.UI.Menus.GamePlay.Components
{
    public sealed class ExperienceBar
    {
        private const float BarDefaultWidth = 0f;
        private const float Epsilon = 0.001f;
        private const float AnimationDuration = 0.5f;

        private VisualElement _expBarBg;
        private VisualElement _expBar;
        private Label _expBarLabel;
        private Label _lvlLabel;
        private float _expToNextLevel;
        private int _currentLevel = 1;
        private float _fullExpWidth;
        private bool _isFullExpWidthSet;
        private float _pxPerPointExp;
        private float _currentExpBarWidth;
        private TweenerCore<float, float, FloatOptions> _expTween;

        private readonly GameUIViewModel _viewModel;
        private readonly VisualElement _root;
        private readonly CompositeDisposable _disposables;

        public ExperienceBar(in GameUIViewModel viewModel, in VisualElement root, in CompositeDisposable disposables)
        {
            _viewModel = viewModel;
            _root = root;
            _disposables = disposables;
        }

        private void SetExpBarWidth(float width)
        {
            if (_isFullExpWidthSet) return;
            _isFullExpWidthSet = true;
            _fullExpWidth = width;
            _pxPerPointExp = _fullExpWidth / _expToNextLevel;
            _currentExpBarWidth = BarDefaultWidth;
            _expBar.style.width = new StyleLength(BarDefaultWidth);
            UpdateExperienceBar(0);
        }

        private void UpdateExperienceBar(int exp)
        {
            if (exp < 0) exp = 0;

            _expBarLabel.text = $"{exp} / {_expToNextLevel}";

            if (!_isFullExpWidthSet) return;

            var targetWidth = _pxPerPointExp * exp;

            if (Math.Abs(targetWidth - _currentExpBarWidth) < Epsilon) return;

            _expTween.Kill();
            _expTween = DOTween.To(
                () => _currentExpBarWidth,
                x =>
                {
                    _currentExpBarWidth = x;
                    _expBar.style.width = x;
                },
                targetWidth,
                AnimationDuration
            );
        }

        public void ResetExperienceBar()
        {
            _expTween.Kill();
            _currentExpBarWidth = BarDefaultWidth;
            _expBar.style.width = new StyleLength(BarDefaultWidth);
            _expBarLabel.text = $"{0} / {_expToNextLevel}";
        }

        public void InitElements()
        {
            _expBarBg = _root.Q<VisualElement>(UIConst.ExpBarContainerIDName);
            _expBarLabel = _expBarBg.Q<Label>(UIConst.ExpBarLabelIDName);
            _expBar = _expBarBg.Q<VisualElement>(UIConst.ExpBarMoveIDName);
            _lvlLabel = _root.Q<Label>(UIConst.LvlLabelIDName);
        }

        public void Init()
        {
            _expBar.RegisterCallback<GeometryChangedEvent>(_ => SetExpBarWidth(_expBar.resolvedStyle.width));

            _viewModel.Level.Subscribe(UpdateLevelLabel).AddTo(_disposables);
            _viewModel.ExpToNextLevel
                .Subscribe(expToNextLevel => { _expToNextLevel = expToNextLevel; })
                .AddTo(_disposables);
            _viewModel.Experience
                .Subscribe(UpdateExperienceBar)
                .AddTo(_disposables);
        }

        private void UpdateLevelLabel(int level)
        {
            _lvlLabel.text = _currentLevel.ToString();
            _currentLevel = level;
        }
    }
}
