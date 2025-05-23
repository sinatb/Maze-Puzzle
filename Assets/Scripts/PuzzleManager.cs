using System.Collections.Generic;
using UnityEngine;
using Util;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { private set; get; }
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

    private void Start()
    {

    }
}
