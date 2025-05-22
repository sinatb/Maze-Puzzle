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

        //Room Type Data
        public RoomType  roomType;

        //Lighting Data
        [HideInInspector]
        public bool[] lightGrid;
        public float lightIntensity;
        
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

            var size = width * height;
            if (lightGrid != null && lightGrid.Length == size) return;
            var newLightGrid = new bool[size];
            for (var i = 0; i < size; i++)
            {
                if (lightGrid != null) 
                    newLightGrid[i] = lightGrid[i];
            }
            lightGrid = newLightGrid;
        }
        
        //Functions
        public void Setup(Vector3 basePosition)
        {
            foreach (var ro in roomObjects)
            {
                var obj = GameManager.Instance.pool.GetPooledObject(ro.tag);
                obj.transform.position = ro.position + basePosition;
                obj.transform.rotation = Quaternion.Euler(ro.rotation);
            }
        }
        
        public bool GetLightingGridValue(int x, int z)
        {
            if (x < 0 || x >= width || z < 0 || z >= height)
            {
                Debug.LogError("Grid index out of bounds");
                return false;
            }
            return lightGrid[z * width + x];
        }

        public void SetLightingGridValue(int x, int z, bool value)
        {
            if (x < 0 || x >= width || z < 0 || z >= height)
            {
                Debug.LogError("Grid index out of bounds");
                return;
            }
            lightGrid[z * width + x] = value;
        }
        
        public void ResizeLightingGrid(int newWidth, int newHeight)
        {
            var oldWidth = width;
            var oldHeight = height;
            width = newWidth;
            height = newHeight;
            var newGrid = new bool[width * height];

            if (lightGrid != null)
            {
                for (int y = 0; y < Mathf.Min(height, oldHeight); y++)
                {
                    for (int x = 0; x < Mathf.Min(width, oldWidth); x++)
                    {
                        newGrid[y * width + x] = GetLightingGridValue(x, y);
                    }
                }
            }

            lightGrid = newGrid;
        }
    }
}