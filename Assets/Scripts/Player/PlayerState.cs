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
    [SerializeField] private float _Sanity = 2000.0f;
    [SerializeField] private Event _gameOverEvent;
    [SerializeField] private Event _gameWinEvent;
    [SerializeField] private float _Score = 0.0f;
    [SerializeField] private float _BaseLoss = 5.0f;
    [SerializeField] private List<float> _LightPenalty;
    [SerializeField] private float _fogPenalty;
    [SerializeField] private float _FlashlightExtra = 3.0f;
    [SerializeField] private float _PuzzleMistakePenalty = -50.0f;
    [SerializeField] private float _PuzzleSolveExtra = 100.0f;
    [SerializeField] private List<RangeRank> _rangeList;
    private int _score = 0;
    private PlayerInventory _playerInventory;
    private int _blockCount;
    private bool _isFog;
    private bool _isGameRunning = true;
    private void updateState() 
    {
        if (_isGameRunning)
        {
            if (_playerInventory.HasItem("flashlight"))
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
        if (_Sanity <= 0.0f && _isGameRunning)
        {
            _gameOverEvent.Raise();
            _isGameRunning = false;
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
                _isGameRunning = false;
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
}
