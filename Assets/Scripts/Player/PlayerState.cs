using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
class RangeRank{
    public int min;
    public int max;
    public String rank;
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
    private float _Sanity = 2000.0f;
    public bool IsGameRunning { 
        private set; get;
    } = true;
    public bool IsGamePaused
    {
        private set; get;
    } = false;
    private void updateState() 
    {
        if (IsGameRunning && !IsGamePaused)
        {
            if (_playerInventory.HasItem("FlashLight"))
            {
                _Sanity += _FlashlightExtra;
            }
            float lp = _isFog ? _fogPenalty : _LightPenalty[_blockCount];
                _Sanity -= _BaseLoss + lp;
        }
    }
    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        InvokeRepeating("updateState", 0.0f,1.0f);
    }

    private void Update()
    {
        if (_Sanity <= 0.0f && IsGameRunning)
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
            
            bool _isGameWon = other.GetComponent<BlockCollider>().GetBlockController().GetIsFinishing();
            if (_isGameWon)
            {
                IsGameRunning = false;
                _gameWinEvent.Raise();
            }
            if (!_isFog)
            {
                _blockCount = other.GetComponent<BlockCollider>().GetBlockController().GetPlayerCount();
                if (_blockCount > 2)
                    _blockCount = 2;
            } 
        }
    }
    public int CalculateWinScore()
    {
        _score += 2000 + (int)Math.Floor(_Sanity);
        return _score;
    }
    public String CalculateRank()
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
        return _Sanity;
    }

    public void PuzzleSolve()
    {
        _score += _PuzzleSolvePoint;
        _Sanity += _PuzzleSolveExtra;
        if (_Sanity > _MaxSanity)
            _Sanity = _MaxSanity;
    }

    public void PuzzleMistake()
    {
        _score -= _PuzzleMistakePoint;
        _Sanity -= _PuzzleMistakePenalty;
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
