using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
public class HangmanPuzzle : Puzzle
{
    [SerializeField] private TextMeshProUGUI _sentenceUI;
    [SerializeField] private TextMeshProUGUI _guessCountUI;
    [SerializeField] private TextMeshProUGUI _cipherUI;

    private string _playerInputKey;
    private string _playerInputValue;
    private List<string> _sentences;
    private Dictionary<char, char> _mapping;
    private string _sentence;
    private bool _exitClicked;
    private bool _solved;
    private int _guessCount;

    public void SetPlayerInputKey(string s)
    {
        _playerInputKey = s;
    }
    public void SetPlayerInputValue(string s) 
    {
        _playerInputValue = s;
    }
    
    private void createCipherUI(string sentence)
    {
        _mapping = new Dictionary<char, char>();
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        foreach (char c in sentence)
        {
            if (char.IsLetter(c)){
                if (!_mapping.ContainsKey(c))
                {
                    int rnd = Random.Range(0, alphabet.Length);
                    _mapping[c] = alphabet[rnd];
                    alphabet = alphabet.Remove(rnd, 1);
                }
                _cipherUI.text += _mapping[c];
            } else {
                _cipherUI.text += c;
            }
        }
    }
    private void createSentenceUI(string sentence)
    {
        foreach(var c in sentence)
        {
            if (Char.IsLetter(c))
            {
                _sentenceUI.text += "-";
            }
            else
            {
                _sentenceUI.text += c;
            }
        }
    }
    private void checkMapping(char k, char v)
    {
        if (!char.IsLetter(k) || !char.IsLetter(v))
            return;
        if (!_sentence.Contains(char.ToLower(k)) && !_sentence.Contains(char.ToUpper(k)))
            return;
        if (!(_mapping.ContainsKey(char.ToLower(k)) && _mapping[char.ToLower(k)] == v) &&
            !(_mapping.ContainsKey(char.ToUpper(k)) && _mapping[char.ToUpper(k)] == v) )
            return;
        int i = 0;
        foreach (var c in  _sentence)
        {
            if (c == char.ToUpper(k) || c == char.ToLower(k))
            {
                _sentenceUI.text = _sentenceUI.text.Substring(0,i) 
                    + c 
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
        string before = _sentenceUI.text;
        if (_playerInputKey != null && _playerInputValue != null)
            checkMapping(_playerInputKey[0], _playerInputValue[0]);
        if (_sentenceUI.text == before)
            _guessCount--;
        _guessCountUI.text = _guessCount.ToString();
        if (!_sentenceUI.text.Contains("-")){
            _solved = true;
            OnPuzzleDone?.Invoke();
        }
        if (_guessCount == 0)
        {
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
        _mapping = new Dictionary<char, char>();

        createSentenceUI(_sentence);
        createCipherUI(_sentence);
        _guessCount = data.GuessCount;
        _guessCountUI.text = _guessCount.ToString();
        string hints = "";
        for (int i=0; i < data.HintCount; i++)
        {
            char rndChar = _sentence[Random.Range(0, _sentence.Length)];
            if (!Char.IsLetter(rndChar) || hints.Contains(rndChar))
            {
                i--;
                continue;
            }
            checkMapping(rndChar, _mapping[rndChar]);
            hints += rndChar;
        }
    }
}
