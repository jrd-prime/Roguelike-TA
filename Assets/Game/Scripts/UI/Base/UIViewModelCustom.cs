using Game.Scripts.UI.Interfaces;
using UnityEngine;
using VContainer;

namespace Game.Scripts.UI.Base
{
    public class UIViewModelCustom<T> : UIViewModelBase where T : class, IUIModel
    {
        protected T Model { get; private set; }

        [Inject]
        private void Construct(T model)
        {
            Debug.LogWarning($"{model} ????");
            Model = model;
        }
    }
}
