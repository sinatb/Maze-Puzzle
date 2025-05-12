using System;
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
        private int                         _playerCount = 0;
        private BlockGraphics               _blockGraphics;

        [SerializeField] private bool       isFogBlock;
        [SerializeField] private bool       isFinishingBlock;
        [SerializeField] private GameObject topWall;
        [SerializeField] private GameObject downWall;
        [SerializeField] private GameObject leftWall;
        [SerializeField] private GameObject rightWall;
        
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
            }
        }
    }
}
