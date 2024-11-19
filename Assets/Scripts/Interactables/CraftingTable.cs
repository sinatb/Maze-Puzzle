using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    public List<Recipe> recipe = new List<Recipe>();
    public string InteractionName => "Craft Flashlight";

    public void Interact(PlayerInteraction pi)
    {
        if (pi.GetComponent<PlayerInventory>().Craft(recipe[0]))
        {
            pi.GetComponent<PlayerInventory>().AddInventoryItem(recipe[0].CreatedItem);
            pi.GetComponent<PlayerController>().ActivateFlashlight();
        }
        else
        {
            pi.GetComponent<PlayerUI>().DisplayAlert("Can't Create Item missing recipe items");
        }
    }
}
