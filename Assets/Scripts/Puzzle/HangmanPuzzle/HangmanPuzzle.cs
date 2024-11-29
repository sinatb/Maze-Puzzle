using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
public class HangmanPuzzle : Puzzle
{
    [SerializeField] private TextMeshProUGUI _sentenceUI;
    [SerializeField] private TextMeshProUGUI _wrongUsedChars;
    [SerializeField] private TextMeshProUGUI _correctUsedChars;

    private string _playerInput;
    private List<string> _sentences;
    private string _sentence;
    private bool _exitClicked;
    private bool _solved;

    public void SetPlayerInput(string s)
    {
        _playerInput = s;
    }
    private void createSentenceUI(string sentence)
    {
        foreach(var c in sentence)
        {
            if (Char.IsLetter(c))
            {
                _sentenceUI.text += "_";
            }
            else
            {
                _sentenceUI.text += c;
            }
        }
    }
    private void checkChar(char c)
    {
        if (!char.IsLetter(c) || 
            (_correctUsedChars.text + _wrongUsedChars.text).Contains(char.ToLower(c)) ||
            (_correctUsedChars.text + _wrongUsedChars.text).Contains(char.ToUpper(c))
            )
            return;
        if (!_sentence.Contains(char.ToLower(c)) && !_sentence.Contains(char.ToUpper(c))){
            _wrongUsedChars.text += c;
            return; 
        }
        int i = 0;
        _correctUsedChars.text += c;
        foreach (var ch in  _sentence)
        {
            if (ch == char.ToUpper(c) || ch == char.ToLower(c))
            {
                _sentenceUI.text = _sentenceUI.text.Substring(0,i) 
                    + ch 
                    + _sentenceUI.text.Substring(i + 1);
            }
            i++;
        }
            
    }

    public void ExitButtonClick()
    {
        _exitClicked = true;
        OnPuzzleDone?.Invoke();
    }
    public void CheckButtonClick()
    {
        _exitClicked = false;
        checkChar(_playerInput[0]);
        if (!_sentenceUI.text.Contains("_")){
            _solved = true;
            OnPuzzleDone?.Invoke();
        }
    }

    public override PuzzleStatus CheckAnswer()
    {
        if (_exitClicked)
        {
            return PuzzleStatus.Unsolved;
        }
        if (_solved)
            return PuzzleStatus.Solved;
        return PuzzleStatus.Mistake;
    }

    public override void Setup(PuzzleData p)
    {
        HangmanPuzzleData data = (HangmanPuzzleData)p;
        _sentences = data.Sentences;
        Chances = data.Chances;
        int num = Random.Range(0, _sentences.Count);
        _sentence = _sentences[num];
        createSentenceUI(_sentence);
        for (int i=0; i < data.HintCount; i++)
        {
            char rndChar = _sentence[Random.Range(0, _sentence.Length)];
            checkChar(rndChar);
        }
    }
}
