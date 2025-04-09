using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle.CodePuzzle
{
    [CreateAssetMenu(fileName = "Code Puzzle Data", menuName = "Puzzle Data/Code Puzzle Data")]
    public class CodePuzzleData : PuzzleData
    {
        [FormerlySerializedAs("StrLen")] public int strLen;
    }
}
