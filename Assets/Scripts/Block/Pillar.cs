using System;
using UnityEngine;

namespace Block
{
    public class Pillar : MonoBehaviour
    {
        private bool _up          = true;
        private bool _down        = true;
        private bool _right       = true;
        private bool _left        = true;
        private int  _activeCount = 4;
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void DeactivateBool(Direction dir)
        {
            _activeCount--;
            switch (dir)
            {
                case Direction.Up :
                    _up = false;
                    break;
                case Direction.Down :
                    _down = false;
                    break;
                case Direction.Right :
                    _right = false;
                    break;
                case Direction.Left :
                    _left = false;
                    break;
                default:
                    return;
            }
            CheckActive();
        }

        private void CheckActive()
        {
            if (_activeCount == 1)
            {
                gameObject.SetActive(true);
                return;
            }
            if (!((_up && _down) || (_right && _left)) && _activeCount == 2)
            {
                gameObject.SetActive(true);
                return;
            }
        }
    }
}