using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _InteractionName = "Open Door";
    public string InteractionName => _InteractionName;

    public void Interact(PlayerInteraction pi)
    {
        
        
    }
}
