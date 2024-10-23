using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string InteractionName { get; }
    void Interact(PlayerInteraction pi);
}
