using System.Collections.Generic;
using Room;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { private set; get;}
    private List<RoomController> _rooms;
    private void Awake()
    {
        _rooms = new List<RoomController>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void AddRoom(RoomController rc)
    {
        _rooms.Add(rc);
    }
    
}
