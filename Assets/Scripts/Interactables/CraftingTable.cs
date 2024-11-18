using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    [SerializeField] private InventoryItemData _flashLight;
    public List<string> recipe = new List<string>()
    {
        "Battery",
        "Flashlight Casing",
        "Tape"
    };
    public string InteractionName => "Craft Flashlight";

    public void Interact(PlayerInteraction pi)
    {
        if (pi.GetComponent<PlayerInventory>().Craft(recipe))
        {
            pi.GetComponent<PlayerInventory>().AddInventoryItem(_flashLight);
            pi.GetComponent<PlayerController>().ActivateFlashlight();
        }
        else
        {
            pi.GetComponent<PlayerUI>().DisplayAlert("Can't Create Item missing recipe items");
        }
    }
}
