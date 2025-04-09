using System.Collections.Generic;
using System.Text;
using TMPro;
using Types;
using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle.CodePuzzle
{
    public class CodePuzzle : Puzzle
    {
        private int _strLen;
        private string _answer;
        private string _playerInput;
        private bool _exitButtonPress;
        [FormerlySerializedAs("_ciphertexts")] [SerializeField] private List<TextMeshProUGUI> ciphertexts =
            new List<TextMeshProUGUI>();
        [FormerlySerializedAs("_plaintexts")] [SerializeField] private List<TextMeshProUGUI> plaintexts =
            new List<TextMeshProUGUI>();
        private string RndString(string alphabet)
        {
            StringBuilder sb = new StringBuilder();
            for (int i=0; i<_strLen; i++)
            {
                int rnd = Random.Range(0, alphabet.Length);
                sb.Append(alphabet[rnd]);
                alphabet = alphabet.Remove(rnd,1);
            }
            return sb.ToString();
        }
        private static string GetNewAlphabet(string alphabet, string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in alphabet)
            {
                if (!text.Contains(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        private void CreatePuzzle()
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            ciphertexts[0].text = RndString(alphabet);
            plaintexts[0].text = RndString(alphabet);
        
            alphabet = GetNewAlphabet(alphabet, ciphertexts[0].text);
            alphabet = GetNewAlphabet(alphabet, plaintexts[0].text);
            ciphertexts[1].text = RndString(alphabet);
            plaintexts[1].text = RndString(alphabet);

            string cipher = ciphertexts[0].text + ciphertexts[1].text;
            string plain = plaintexts[0].text + plaintexts[1].text;
            ciphertexts[2].text = RndString(cipher);

            Dictionary<char,char> dict = new Dictionary<char,char>();
            int i = 0;
            foreach (var c in cipher)
            {
                dict[c] = plain[i];
                i++;
            }
            foreach (var c in ciphertexts[2].text)
            {
                _answer += dict[c];
            }
        }

        public void SetPlayerInput(string s)
        {
            _playerInput = s;
        }
        public void SubmitButtonClick()
        {
            _exitButtonPress = false;
            OnPuzzleDone?.Invoke();
        }
        public void ExitButtonClick()
        {
            _exitButtonPress = true;
            OnPuzzleDone?.Invoke();
        }
        public override PuzzleStatus CheckAnswer()
        {
            if (_exitButtonPress)
            {
                return PuzzleStatus.Unsolved;
            }
            if (_answer == _playerInput)
            {
                return PuzzleStatus.Solved;
            }
            return PuzzleStatus.Mistake;
        }

        public override void Setup(PuzzleData p)
        {
            CodePuzzleData data = (CodePuzzleData)p;
            Chances = data.chances;
            _strLen = data.strLen;
            CreatePuzzle();
        }
    }
}
