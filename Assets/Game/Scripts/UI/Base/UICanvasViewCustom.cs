using System;
using Game.Scripts.Framework.Managers.Experience;
using Game.Scripts.Framework.Scopes;
using Game.Scripts.UI.Menus.Interfaces;
using UnityEngine;
using VContainer;

namespace Game.Scripts.UI.Base
{
    public abstract class UICanvasViewCustom<T> : UICanvasView where T : IUIViewModel
    {
        protected T ViewModel;

        protected RectTransform rectTransform;

        protected IExperienceManager ExperienceManager { get; private set; }

        [Inject]
        private void Construct(T viewModel, IExperienceManager experienceManager)
        {
            Debug.LogWarning("construct");
            ViewModel = viewModel;
            ExperienceManager = experienceManager;
        }

        private void Start()
        {
            if (ViewModel == null)
                throw new NullReferenceException($"ViewModel in {name} is null. Check container registration!");


            rectTransform = GetComponent<RectTransform>();
            ApplySafeArea();
        }

        void ApplySafeArea()
        {
            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}
