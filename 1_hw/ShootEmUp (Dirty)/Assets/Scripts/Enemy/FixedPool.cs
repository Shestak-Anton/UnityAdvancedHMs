using System.Collections.Generic;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class FixedPool : MonoBehaviour, ILifeCycle.ICreateListener
    {
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _size = 7;

        private readonly Queue<GameObject> _pool = new();

        void ILifeCycle.ICreateListener.OnCreate()
        {
            FillPool(_size);
        }

        public bool TryDequeue(out GameObject pooledGameObject)
        {
            return _pool.TryDequeue(out pooledGameObject);
        }

        public void Enqueue(GameObject go)
        {
            go.transform.SetParent(_container);
            _pool.Enqueue(go);
        }

        private void FillPool(int poolSize)
        {
            for (var i = 0; i < poolSize; i++)
            {
                var enemy = Instantiate(_prefab, _container);
                _pool.Enqueue(enemy);
            }
        }
    }
}