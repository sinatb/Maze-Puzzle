using System.Collections.Generic;
using Block;
using UnityEngine;

namespace Room
{
    public class RoomController : MonoBehaviour
    {
        private RoomType         _roomType;
        private List<GameObject> _roomObjects;
        public void Setup(RoomType type)
        {
            _roomType = type;
            _roomObjects = new List<GameObject>();
            RoomManager.Instance.AddRoom(this);
        }
        public void AddBlock(GameObject block)
        {
            var blockController = block.GetComponent<BlockController>();
            blockController.SetRoom(this);
            _roomObjects.Add(block);
        }

        public void SetLight(int index, float intensity)
        {
            _roomObjects[index].GetComponent<BlockGraphics>().ChangeLightIntensity(intensity);
        }
        
    }
}