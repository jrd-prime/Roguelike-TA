using System;
using Game.Scripts.Framework.Managers.Experience;
using Game.Scripts.Framework.Scopes;
using Game.Scripts.UI.Menus.Interfaces;
using VContainer;

namespace Game.Scripts.UI.Base
{
    public abstract class UIViewCustom<T> : UIViewBase where T : IUIViewModel
    {
        protected T ViewModel;

        protected IExperienceManager ExperienceManager { get; private set; }

        [Inject]
        private void Construct(T viewModel, IExperienceManager experienceManager)
        {
            ViewModel = viewModel;
            ExperienceManager = experienceManager;
        }

        private void Start()
        {
            if (ViewModel == null)
                throw new NullReferenceException($"ViewModel in {name} is null. Check container registration!");
        }
    }
}
