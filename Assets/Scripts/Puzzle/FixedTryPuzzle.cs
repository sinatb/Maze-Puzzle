using UnityEngine;

namespace Puzzle
{
    public abstract class FixedTryPuzzle : Puzzle
    {
        [SerializeField] private int maxTries;
        private int _tries = 0;
        public override bool CanSolve()
        {
            if (_tries < maxTries)
                return true;
            return false;
        }
        public void IncrementTries()
        {
            _tries++;
        }
    }
}