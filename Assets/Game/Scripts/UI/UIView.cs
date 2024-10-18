using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class UIView : MonoBehaviour
    {
        protected VisualElement RootVisualElement;

        private void Awake()
        {
            var uiDocument = gameObject.GetComponent<UIDocument>();

            Assert.IsNotNull(uiDocument.visualTreeAsset, "VisualTreeAsset is not set to " + name + " prefab!");

            RootVisualElement = uiDocument.rootVisualElement;

            gameObject.SetActive(false);
        }
    }
}
