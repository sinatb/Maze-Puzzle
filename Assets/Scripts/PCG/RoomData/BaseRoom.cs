using System;
using System.Collections.Generic;
using Block;
using UnityEngine;

namespace PCG.RoomData
{
    [Serializable]
    public struct ObjectData
    {
        public string tag;
        public GameObject prefab;
        public Vector3 position;
        public Vector3 rotation;
    }
    [CreateAssetMenu (fileName = "RoomData", menuName = "PCG/RoomData")]
    public class BaseRoom : ScriptableObject
    {
        //Basic Measurements
        public int width;
        public int height;
        
        //Entrance Location
        public int        doorX;
        public int        doorZ;
        public Direction  doorDirection;
        
        //Room Objects
        public List<ObjectData> roomObjects;

        void OnValidate()
        {
            if (doorX < 0 || doorX >= width)
            {
                Debug.LogError("Door X must be between 0 and width - 1");
            }
            if (doorZ < 0 || doorZ >= height)
            {
                Debug.LogError("Door Z must be between 0 and height - 1");
            }
            if (doorX != 0 && doorX != width - 1)
            {
                if (doorZ != 0 && doorZ != height - 1)
                {
                    Debug.LogError("Door X must be 0 or width - 1");
                }
            }
            if (doorZ != 0 && doorZ != height - 1)
            {
                if (doorX != 0 && doorX != width - 1)
                {
                    Debug.LogError("Door Z must be 0 or height - 1");
                }
            }
        }
        
        //Functions
        public void Setup(Vector3 basePosition)
        {
            foreach (var ro in roomObjects)
            {
                Instantiate(ro.prefab,
                     ro.position + basePosition,
                     Quaternion.Euler(ro.rotation));
            }
        }
    }
}