using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    public List<string> recipe = new List<string>()
    {
        "Battery",
        "Flashlight Casing",
        "Optical Lens"
    };
    public string InteractionName => "Craft Flashlight";

    public void Interact(PlayerInteraction pi)
    {
        if (pi.GetComponent<PlayerInventory>().CheckDependency(recipe))
        {
            Debug.Log("making Flashlight");
        }
        else
        {
            pi.GetComponent<PlayerUI>().DisplayAlert("Can't Create Item missing recipe items");
        }
    }
}
