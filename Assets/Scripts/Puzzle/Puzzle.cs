using UnityEngine;

namespace Puzzle
{
    public abstract class Puzzle : MonoBehaviour  
    {
        protected callback OnPuzzleDone;
        public abstract bool CanSolve();
        public void SetCallback(callback c) {
            OnPuzzleDone += c;
        }
        public abstract void Setup(PuzzleData p);
        public abstract PuzzleStatus CheckAnswer();

    }
}
