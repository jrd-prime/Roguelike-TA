using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class PopUpView : MonoBehaviour
    {
        private VisualElement _rootVisualElement;
        private Label _popUpLabel;

        private void Awake()
        {
            var document = gameObject.GetComponent<UIDocument>();

            _rootVisualElement = document.visualTreeAsset != null
                ? document.rootVisualElement
                : throw new NullReferenceException("VisualTreeAsset is not set to " + name + " prefab!");

            _popUpLabel = _rootVisualElement.Q<Label>("pop-up-label");
            Assert.IsNotNull(_popUpLabel, "PopUpLabel is not set to " + name + " prefab!");

            _rootVisualElement.style.display = DisplayStyle.None;
        }

        public void Show(string lowPriorityWillBeImplementedLater)
        {
            _popUpLabel.text = lowPriorityWillBeImplementedLater;
            _rootVisualElement.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            _rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}
