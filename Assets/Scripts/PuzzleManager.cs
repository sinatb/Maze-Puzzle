using System.Collections.Generic;
using Interactables;
using NUnit.Framework;
using UnityEngine;
using Util;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { private set; get; }
    [SerializeField] private int numPuzzles;
    [SerializeField] private List<GameObject> puzzlePrefabs;
    private List<Door> _doors;
    
    private void Awake()
    {
        _doors = new List<Door>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void RegisterDoor(Door d)
    {
        _doors.Add(d);
        if (DebugUtil.Instance.showDebugData)
        {
            Debug.Log("Number of Doors In Puzzle Manager : " + _doors.Count);
        }
    }

    public void SetPuzzles()
    {
        foreach (var d in _doors)
        {
            d.AddPuzzle(puzzlePrefabs[0]);
        }
    }
}
