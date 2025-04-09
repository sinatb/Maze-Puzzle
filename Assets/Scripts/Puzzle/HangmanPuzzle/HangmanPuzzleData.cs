using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.HangmanPuzzle
{
    [CreateAssetMenu(fileName ="Hangman Puzzle Data",menuName = "Puzzle Data/Hangman Puzzle Data")]
    public class HangmanPuzzleData : PuzzleData
    {
        public List<string> Sentences;
        public int HintCount;
        public int GuessCount;
    }
}
