using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TripleTextPuzzle : Puzzle
{
    [SerializeField] private TextMeshProUGUI _question;
    [SerializeField] private TextMeshProUGUI _buttonText;
    private List<string> _questions;
    private List<string> _answers;
    private string _playerInput;
    private int _num = 0;
    private bool _fail = false;
    private void OnEnable()
    {
        if (_questions != null) 
            setPuzzleState(_num);
    }
    private void setPuzzleState(int _num) 
    {
        _question.text = _questions[_num];
        if (_num < 2)
        {
            _buttonText.text = "Next";
        }
        else
        {
            _buttonText.text = "Submit";
        }
    }
    public void SetPlayerInput(string s)
    {
        _playerInput = s;
    }

    public void ButtonClick()
    {
        if (_num == 2)
        {
            OnPuzzleDone?.Invoke();
        }
        else
        {
            Debug.Log(_playerInput);
            if (_playerInput == _answers[_num])
            {
                _num++;
                setPuzzleState(_num);
            }
            else
            {
                _fail = true;
                OnPuzzleDone?.Invoke();
            }
        }
    }
    public override PuzzleStatus CheckAnswer()
    {
        if (_fail)
        {
            _fail = false;
            _num = 0;
            return PuzzleStatus.Mistake;
        }
        if (_playerInput == _answers[_num])
        {
            _fail = false;
            _num = 0;
            return PuzzleStatus.Solved;
        }
        _fail = false;
        _num = 0;
        return PuzzleStatus.Mistake;
    }

    public override void Setup(PuzzleData p)
    {
        TripleTextPuzzleData data = (TripleTextPuzzleData)p;
        _questions = new List<string> { data.Q1,data.Q2,data.Q3};
        _answers = new List<string> { data.A1,data.A2,data.A3};
        setPuzzleState(_num);
    }
}
