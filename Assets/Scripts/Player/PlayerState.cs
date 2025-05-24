using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [Serializable]
    internal class RangeRank{
        public int min;
        public int max;
        public string rank;
    }
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private float _MaxSanity = 2000.0f;
        [SerializeField] private Event _gameOverEvent;
        [SerializeField] private Event _gameWinEvent;
        [SerializeField] private Event _gamePause;
        [SerializeField] private Event _gameUnPause;
        [SerializeField] private float _BaseLoss = 5.0f;
        [SerializeField] private List<float> _LightPenalty;
        [SerializeField] private float _fogPenalty;
        [SerializeField] private float _FlashlightExtra = 3.0f;
        [SerializeField] private float _PuzzleMistakePenalty = 50.0f;
        [SerializeField] private float _PuzzleSolveExtra = 100.0f;
        [SerializeField] private int _PuzzleSolvePoint = 60;
        [SerializeField] private int _PuzzleMistakePoint = 10;
        [SerializeField] private List<RangeRank> _rangeList;
        private int _score = 0;
        private PlayerInventory _playerInventory;
        private int _blockCount;
        private bool _isFog;
        private float _sanity = 2000.0f;
        public bool IsGameRunning { 
            private set; get;
        } = true;
        public bool IsGamePaused
        {
            private set; get;
        } = false;
        private void UpdateState()
        {
            if (!IsGameRunning) return;
            if (_playerInventory.HasItem("FlashLight"))
            {
                _sanity += _FlashlightExtra;
            }
            var lp = _isFog ? _fogPenalty : _LightPenalty[_blockCount];
            _sanity -= _BaseLoss + lp;
        }
        private void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            InvokeRepeating(nameof(UpdateState), 0.0f,1.0f);
        }

        private void Update()
        {
            if (_sanity <= 0.0f && IsGameRunning)
            {
                _gameOverEvent.Raise();
                IsGameRunning = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("EntranceCollider"))
            {
                _isFog = other.GetComponent<BlockCollider>().GetBlockController().GetIsFog();
            
                var isGameWon = other.GetComponent<BlockCollider>().GetBlockController().GetIsFinishing();
                if (isGameWon)
                {
                    IsGameRunning = false;
                    _gameWinEvent.Raise();
                }

                if (_isFog) return;
                _blockCount = other.GetComponent<BlockCollider>().GetBlockController().GetPlayerCount();
                if (_blockCount > 2)
                    _blockCount = 2;
            }
        }
        public int CalculateWinScore()
        {
            _score += 2000 + (int)Math.Floor(_sanity);
            return _score;
        }
        public string CalculateRank()
        {
            foreach (var rl in _rangeList)
            {
                if (_score >= rl.min && _score <= rl.max)
                    return rl.rank;
            }
            return "";
        }
        public float GetSanity()
        {
            return _sanity;
        }

        public void PuzzleSolve()
        {
            _score += _PuzzleSolvePoint;
            _sanity += _PuzzleSolveExtra;
            if (_sanity > _MaxSanity)
                _sanity = _MaxSanity;
        }

        public void PuzzleMistake()
        {
            _score -= _PuzzleMistakePoint;
            _sanity -= _PuzzleMistakePenalty;
            GetComponent<PlayerUI>().DisplayAlert("Wrong Answer");
        }
        public void PauseGame()
        {
            _gamePause.Raise();
            IsGamePaused = true;
        }
        public void UnpauseGame()
        {
            _gameUnPause.Raise();
            IsGamePaused = false;
        }

        public int GetScore()
        {
            return _score;
        }
    }
}