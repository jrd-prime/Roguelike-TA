using System;
using UnityEngine;

namespace Game.Scripts.UI.Base
{
    [RequireComponent(typeof(Canvas))]
    public abstract class UICanvasView : UIViewBase
    {
        protected Canvas CanvasRoot;

        public void Awake()
        {
            var canvas = gameObject.GetComponent<Canvas>();

            CanvasRoot = canvas.rootCanvas != null
                ? canvas.rootCanvas
                : throw new NullReferenceException("Canvas root is null!");

            InitElements();
            Init();
            InitCallbacksCache();
            Hide();
        }

        public override void Show()
        {
            // throw new NotImplementedException();

            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            // throw new NotImplementedException();
            gameObject.SetActive(false);
        }
    }
}
