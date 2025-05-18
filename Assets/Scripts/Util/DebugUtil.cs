using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Util
{
    public class DebugUtil : MonoBehaviour
    {
        public bool showDebugData;
        public static DebugUtil Instance { get; private set;}

        [SerializeField] private int objectCount;
        
        private List<GameObject> _debugObjects;

        
        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        
        private void Start()
        {
            _debugObjects = new List<GameObject>(objectCount);
            for (var i = 0; i < objectCount; i++)
            {
                var go = new GameObject();
                var tmp = go.AddComponent<TextMeshPro>();
                go.transform.SetParent(transform);
                go.SetActive(false);
                tmp.alignment = TextAlignmentOptions.Center;
                _debugObjects.Add(go);
            }
        }

        private GameObject GetDebugObject()
        {
            return _debugObjects.FirstOrDefault(go => go.activeSelf == false);
        }
        public void DrawDebugText(Vector3 pos,string txt, Color color = default, int size = 30)
        {
            if (!showDebugData)
                return;
            if (color == default)
                color = Color.red;

            var go = GetDebugObject();
            go.transform.position = pos;
            var tmp = go.GetComponent<TextMeshPro>();
            tmp.text = txt;
            tmp.color = color;
            tmp.fontSize = size;
            go.SetActive(true);
        }
    }
}