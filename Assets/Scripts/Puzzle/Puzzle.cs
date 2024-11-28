using UnityEngine;
public abstract class Puzzle : MonoBehaviour  
{
    protected callback OnPuzzleDone;
    public void SetCallback(callback c) {
        OnPuzzleDone += c;
    }
    public abstract void Setup(PuzzleData p);
    public abstract PuzzleStatus CheckAnswer();

}
