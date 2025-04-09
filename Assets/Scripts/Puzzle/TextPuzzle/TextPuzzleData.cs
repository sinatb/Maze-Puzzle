using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle.TextPuzzle
{
    [CreateAssetMenu(fileName ="Text Puzzle Data",menuName ="Puzzle Data/Text Puzzle Data")]
    public class TextPuzzleData : PuzzleData
    {
        [FormerlySerializedAs("Question")] public string question;
        [FormerlySerializedAs("Answer")] public string answer;
    }
}
