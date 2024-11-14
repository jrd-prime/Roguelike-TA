using System;
using System.Collections;
using Game.Scripts.Framework.Providers.Pools;
using UnityEngine;
using VContainer;

namespace Game.Scripts.UI.PopUpText
{
    public class PopUpTextManager : MonoBehaviour
    {
        [SerializeField] private PopUpTextHolder popUpTextHolderPrefab;

        private CustomPool<PopUpTextHolder> _popUpTextHolderPool;
        private IObjectResolver _resolver;

        [Inject]
        private void Construct(IObjectResolver resolver) => _resolver = resolver;

        private void Awake()
        {
            if (_resolver == null) throw new NullReferenceException("Resolver is null. Add this to gamecontext.");
            if (popUpTextHolderPrefab == null) throw new NullReferenceException("PopUpTextHolderPrefab is not set.");

            _popUpTextHolderPool =
                new CustomPool<PopUpTextHolder>(popUpTextHolderPrefab, 10, transform, _resolver);
        }

        public void ShowPopUpText(string text, Vector3 position, float durationSeconds, bool isCrit)
        {
            var popUpTextHolder = _popUpTextHolderPool.Get();

            popUpTextHolder.transform.position = position;

            popUpTextHolder.gameObject.SetActive(true);

            popUpTextHolder.Show(text, durationSeconds, isCrit);
            popUpTextHolder.StartCoroutine(PopUpDuration(popUpTextHolder, durationSeconds));
        }

        private IEnumerator PopUpDuration(PopUpTextHolder popUpTextHolder, float durationSeconds)
        {
            yield return new WaitForSeconds(durationSeconds);
            _popUpTextHolderPool.Return(popUpTextHolder);
        }
    }
}
