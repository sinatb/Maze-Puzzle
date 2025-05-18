using System.Collections.Generic;
using UnityEngine;

namespace Block
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public class BlockController : MonoBehaviour
    {
        private BlockType                   _blockType;
        private int                         _playerCount = 0;
        private BlockGraphics               _blockGraphics;
        private bool                        _visited;

        [SerializeField] private bool       isFogBlock;
        [SerializeField] private bool       isFinishingBlock;
        // Walls
        [SerializeField] private GameObject topWall;
        [SerializeField] private GameObject downWall;
        [SerializeField] private GameObject leftWall;
        [SerializeField] private GameObject rightWall;
        // Doors
        [SerializeField] private GameObject rightDoor;
        [SerializeField] private GameObject leftDoor;
        [SerializeField] private GameObject topDoor;
        [SerializeField] private GameObject downDoor;
        // Pillar
        [SerializeField] private Pillar pillar;
        private void Awake()
        {
            _blockGraphics = GetComponent<BlockGraphics>();
            _blockGraphics.Setup(isFogBlock);
        }
        public void IncPlayerCount()
        {
            _playerCount++;
            _blockGraphics.OnIncreaseCount(_playerCount);
        }
        public int GetPlayerCount()
        {
            return _playerCount;
        }
        public bool GetIsFog()
        {
            return isFogBlock;
        }
        public bool GetIsFinishing()
        {
            return isFinishingBlock;
        }
        public void SetBlockType(BlockType blockType)
        {
            _blockType = blockType;
        }
        public bool IsCorridor()
        {
            return _blockType == null;
        }
        // PCG Related Functions
        public void ClearWall(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    topWall.SetActive(false);
                    break;
                case Direction.Down:
                    downWall.SetActive(false);
                    break;
                case Direction.Left:
                    leftWall.SetActive(false);
                    break;
                case Direction.Right:
                    rightWall.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        public void EnableDoor(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                   topWall.SetActive(false);
                   topDoor.SetActive(true);
                   break;
                case Direction.Down:
                    downWall.SetActive(false);
                    downDoor.SetActive(true);
                    break;
                case Direction.Left:
                    leftWall.SetActive(false);
                    leftDoor.SetActive(true);
                    break;
                case Direction.Right:
                    rightWall.SetActive(false);
                    rightDoor.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        public bool IsVisited()
        {
            return _visited;
        }
        public void SetVisited()
        {
            _visited = true;
        }
        public Pillar GetPillar()
        {
            return pillar;
        }
    }
}
