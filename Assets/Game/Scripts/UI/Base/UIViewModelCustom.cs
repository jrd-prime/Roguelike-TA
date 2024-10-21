using System;
using Game.Scripts.UI.Interfaces;
using Game.Scripts.UI.Models;
using UnityEngine;
using VContainer;

namespace Game.Scripts.UI.Base
{
    public abstract class UIViewModelCustom<T> : UIViewModelBase where T : class, IUIModel
    {
        protected T Model { get; private set; }

        [Inject]
        private void Construct(T model)
        {
            Model = model;

            if (Model == null) throw new NullReferenceException($"{typeof(T)} is null");
        }
    }
}
