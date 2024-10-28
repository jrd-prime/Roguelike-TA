using System;
using Game.Scripts.Framework.Managers.Experience;
using Game.Scripts.Framework.Scopes;
using Game.Scripts.UI.Menus.Interfaces;
using UnityEngine;
using VContainer;

namespace Game.Scripts.UI.Base
{
    public abstract class UIViewModelCustom<T> : UIViewModelBase where T : class, IUIModel
    {
        protected static T Model { get; private set; }

        [Inject]
        private void Construct(T model)
        {
            Model = model;

            if (Model == null) throw new NullReferenceException($"{typeof(T)} is null");
        }
    }
}
