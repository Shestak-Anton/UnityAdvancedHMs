using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class FixedPool : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int size = 7;

        private readonly Queue<GameObject> _pool = new();

        private void Awake()
        {
            FillPool(size);
        }

        public bool TryDequeue(out GameObject pooledGameObject)
        {
            return _pool.TryDequeue(out pooledGameObject);
        }

        public void Enqueue(GameObject go)
        {
            go.transform.SetParent(container);
            _pool.Enqueue(go);
        }

        private void FillPool(int poolSize)
        {
            for (var i = 0; i < poolSize; i++)
            {
                var enemy = Instantiate(prefab, container);
                _pool.Enqueue(enemy);
            }
        }
    }
}