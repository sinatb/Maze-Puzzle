using Types;
using UnityEngine;

namespace Puzzle
{
    public abstract class Puzzle : MonoBehaviour  
    {
        protected Callback OnPuzzleDone;
        protected int Chances;

        public int ReduceChances()
        {
            Chances--;
            return Chances;
        }
        public void SetCallback(Callback c) {
            OnPuzzleDone += c;
        }
        public abstract void Setup(PuzzleData p);
        public abstract PuzzleStatus CheckAnswer();

    }
}
