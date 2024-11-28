using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPuzzle : Puzzle
{
    [SerializeField] private TextMeshProUGUI _puzzleText;
    private string _playerInput;
    private string _answer;

    public override void SetCallback(callback c)
    {
        OnPuzzleDone += c;
    }
    public override PuzzleStatus CheckAnswer()
    {
        if (_answer == _playerInput)
        {
            return PuzzleStatus.Solved;
        }
        return PuzzleStatus.Mistake;
    }

    public void SetPlayerInput(string s)
    {
        _playerInput = s;
    }

    public void ButtonClick()
    {
        OnPuzzleDone?.Invoke();
    }

    public override void Setup(PuzzleData p)
    {
        TextPuzzleData data = (TextPuzzleData) p;
        _puzzleText.text = data.Question;
        _answer = data.Answer;
    }
}
