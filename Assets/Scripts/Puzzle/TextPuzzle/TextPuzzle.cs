using TMPro;
using Types;
using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle.TextPuzzle
{
    public class TextPuzzle : Puzzle
    {
        [FormerlySerializedAs("_puzzleText")] [SerializeField] private TextMeshProUGUI puzzleText;
        private string _playerInput;
        private string _answer;

        public override PuzzleStatus CheckAnswer()
        {
            return _answer == _playerInput ? PuzzleStatus.Solved : PuzzleStatus.Mistake;
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
            var data = (TextPuzzleData) p;
            puzzleText.text = data.question;
            _answer = data.answer;
            Chances = data.chances;
        }
    }
}
