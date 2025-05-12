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