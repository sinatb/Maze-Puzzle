using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Types;
using UnityEngine;

namespace Pooling
{
    public class ObjectPool : MonoBehaviour
    {
        public bool isReady;
        
        [SerializeField] private List<PoolingData> poolingData;
        
        private Dictionary<string, List<GameObject>> _pool;
        private Dictionary<string, List<GameObject>> _activePool;
        private void Start()
        {
            _pool = new Dictionary<string, List<GameObject>>();
            
            foreach (var pd in poolingData)
            {
                var pdParent = new GameObject(pd.name);
                for (var i = 0; i < pd.count; i++)
                {
                    var go = Instantiate(pd.prefab, pdParent.transform);
                    go.SetActive(false);
                    if (!_pool.ContainsKey(pd.name))
                    {
                        _pool[pd.name] = new List<GameObject>();
                    }
                    _pool[pd.name].Add(go);
                }
            }
            isReady = true;
        }

        public GameObject GetPooledObject(string key)
        {
            foreach (var g in _pool[key].Where(g => !g.activeSelf))
            {
                g.SetActive(true);
                return g;
            }
            return null;
        }
    }
}