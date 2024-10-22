using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Framework
{
    public class CustomPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly bool _allowGrowth;
        private int _poolSize;
        private readonly Transform _parent;

        private readonly Queue<T> _cache = new();
        private readonly HashSet<T> _activeObjects = new(); // Для проверки активных объектов

        public CustomPool(T prefab, int poolSize, Transform parent, bool allowGrowth = false)
        {
            _prefab = prefab;
            _poolSize = poolSize;
            _parent = parent;
            _allowGrowth = allowGrowth;

            Initialize();
        }

        private void Initialize()
        {
            for (var i = 0; i < _poolSize; i++) CreateObject();
        }

        private void CreateObject()
        {
            var go = Object.Instantiate(_prefab, _parent);
            go.gameObject.SetActive(false);
            _cache.Enqueue(go);
        }

        public T Get()
        {
            if (_cache.Count > 0)
            {
                var obj = _cache.Dequeue();
                obj.gameObject.SetActive(false);
                _activeObjects.Add(obj);
                return obj;
            }

            if (!_allowGrowth) throw new NullReferenceException("Пул пуст и рост пула не разрешен!");

            var newObj = Object.Instantiate(_prefab, _parent);
            newObj.gameObject.SetActive(false);
            _activeObjects.Add(newObj);
            return newObj;
        }

        public void Return(T obj)
        {
            if (obj == null)
            {
                Debug.LogError("Can't return null object to pool!");
                return;
            }

            if (_activeObjects.Contains(obj))
            {
                obj.gameObject.SetActive(false);
                _activeObjects.Remove(obj);
                _cache.Enqueue(obj);
            }
            else Debug.LogError("Object was taken not from this pool!");
        }

        public int GetAvailableCount()
        {
            return _cache.Count;
        }

        public int GetActiveCount()
        {
            return _activeObjects.Count;
        }
    }
}
