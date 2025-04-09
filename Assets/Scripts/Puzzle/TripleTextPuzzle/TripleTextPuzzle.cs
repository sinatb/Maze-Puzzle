using System.Collections.Generic;
using TMPro;
using Types;
using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle.TripleTextPuzzle
{
    public class TripleTextPuzzle : Puzzle
    {
        [FormerlySerializedAs("_question")] [SerializeField] private TextMeshProUGUI question;
        [FormerlySerializedAs("_buttonText")] [SerializeField] private TextMeshProUGUI buttonText;
        private List<string> _questions;
        private List<string> _answers;
        private string _playerInput;
        private int _num = 0;
        private bool _fail = false;
        private void OnEnable()
        {
            if (_questions != null) 
                SetPuzzleState(_num);
        }
        private void SetPuzzleState(int num)
        {
            question.text = _questions[num];
            buttonText.text = num < 2 ? "Next" : "Submit";
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
                if (_playerInput == _answers[_num])
                {
                    _num++;
                    SetPuzzleState(_num);
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
            Chances = data.chances;
            _questions = new List<string> { data.q1,data.q2,data.q3};
            _answers = new List<string> { data.a1,data.a2,data.a3};
            SetPuzzleState(_num);
        }
    }
}
