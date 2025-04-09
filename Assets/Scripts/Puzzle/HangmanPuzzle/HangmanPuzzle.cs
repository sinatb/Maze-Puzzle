using System;
using System.Collections.Generic;
using TMPro;
using Types;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
namespace Puzzle.HangmanPuzzle
{
    public class HangmanPuzzle : Puzzle
    {
        [FormerlySerializedAs("_sentenceUI")] [SerializeField] private TextMeshProUGUI sentenceUI;
        [FormerlySerializedAs("_guessCountUI")] [SerializeField] private TextMeshProUGUI guessCountUI;
        [FormerlySerializedAs("_cipherUI")] [SerializeField] private TextMeshProUGUI cipherUI;

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
    
        private void CreateCipherUI(string sentence)
        {
            _mapping = new Dictionary<char, char>();
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            foreach (var c in sentence)
            {
                if (char.IsLetter(c)){
                    if (!_mapping.ContainsKey(c))
                    {
                        var rnd = Random.Range(0, alphabet.Length);
                        _mapping[c] = alphabet[rnd];
                        alphabet = alphabet.Remove(rnd, 1);
                    }
                    cipherUI.text += _mapping[c];
                } else {
                    cipherUI.text += c;
                }
            }
        }
        private void CreateSentenceUI(string sentence)
        {
            foreach(var c in sentence)
            {
                if (char.IsLetter(c))
                {
                    sentenceUI.text += "-";
                }
                else
                {
                    sentenceUI.text += c;
                }
            }
        }
        private void CheckMapping(char k, char v)
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
                    sentenceUI.text = sentenceUI.text[..i] 
                                       + c 
                                       + sentenceUI.text[(i + 1)..];
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
            string before = sentenceUI.text;
            if (_playerInputKey != null && _playerInputValue != null)
                CheckMapping(_playerInputKey[0], _playerInputValue[0]);
            if (sentenceUI.text == before)
                _guessCount--;
            guessCountUI.text = _guessCount.ToString();
            if (!sentenceUI.text.Contains("-")){
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
            Chances = data.chances;
            int num = Random.Range(0, _sentences.Count);
            _sentence = _sentences[num];
            _mapping = new Dictionary<char, char>();

            CreateSentenceUI(_sentence);
            CreateCipherUI(_sentence);
            _guessCount = data.GuessCount;
            guessCountUI.text = _guessCount.ToString();
            var hints = "";
            for (var i=0; i < data.HintCount; i++)
            {
                var rndChar = _sentence[Random.Range(0, _sentence.Length)];
                if (!char.IsLetter(rndChar) || hints.Contains(rndChar))
                {
                    i--;
                    continue;
                }
                CheckMapping(rndChar, _mapping[rndChar]);
                hints += rndChar;
            }
        }
    }
}
