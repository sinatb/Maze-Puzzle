using UnityEngine;
public abstract class Puzzle : MonoBehaviour  
{
    protected callback OnPuzzleDone;
    protected int Chances;

    public int ReduceChances()
    {
        Chances--;
        return Chances;
    }
    public void SetCallback(callback c) {
        OnPuzzleDone += c;
    }
    public abstract void Setup(PuzzleData p);
    public abstract PuzzleStatus CheckAnswer();

}
