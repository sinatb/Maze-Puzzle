using UnityEngine;
public abstract class Puzzle : MonoBehaviour  
{
    protected callback OnPuzzleDone;
    public abstract void SetCallback(callback c);
    public abstract void Setup(PuzzleData p);
    public abstract bool CheckAnswer();

}
