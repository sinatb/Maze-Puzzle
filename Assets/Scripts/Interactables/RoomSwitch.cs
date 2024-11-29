using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSwitch : MonoBehaviour, IInteractable
{
    public GameObject OtherRoom;
    public string InteractionName => "Press E to Switch Room";

    public void Interact(PlayerInteraction pi)
    {
        pi.transform.position = new Vector3(OtherRoom.transform.position.x,
                                            pi.transform.position.y,
                                            OtherRoom.transform.position.z);
    }
}
