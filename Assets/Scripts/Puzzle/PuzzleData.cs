using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle
{
    public class PuzzleData : ScriptableObject
    {
        [FormerlySerializedAs("Chances")] public int chances;
    }
}
