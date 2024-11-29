using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Hangman Puzzle Data",menuName = "Puzzle Data/Hangman Puzzle Data")]
public class HangmanPuzzleData : PuzzleData
{
    public List<string> Sentences;
    public int HintCount;
}
