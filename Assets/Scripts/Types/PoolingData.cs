using System;
using UnityEngine;

namespace Types
{
    [Serializable]
    public class PoolingData
    {
        public string     name;
        public GameObject prefab;
        public int        count;
    }
}