namespace Puzzle
{
    public abstract class FixedTryPuzzle : Puzzle
    {
        public int chances;
        public override bool CanSolve()
        {
            if (chances <= 0) return false;
            chances -= 1;
            return true;
        }
    }
}