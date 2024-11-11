using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour, IInteractable
{
    public InventoryItemData data;
    public string InteractionName => "Pick Up";

    public void Interact(PlayerInteraction pi)
    {
        pi.GetComponent<PlayerInventory>().AddInventoryItem(data);
        gameObject.SetActive(false);
    }
}
