using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle.TripleTextPuzzle
{
    [CreateAssetMenu(fileName = "Triple Text Puzzle", menuName = "Puzzle Data/Triple Text Puzzle")]
    public class TripleTextPuzzleData : PuzzleData
    {
        [FormerlySerializedAs("Q1")] public string q1;
        [FormerlySerializedAs("Q2")] public string q2;
        [FormerlySerializedAs("Q3")] public string q3;
        [FormerlySerializedAs("A1")] public string a1;
        [FormerlySerializedAs("A2")] public string a2;
        [FormerlySerializedAs("A3")] public string a3;
    }
}
