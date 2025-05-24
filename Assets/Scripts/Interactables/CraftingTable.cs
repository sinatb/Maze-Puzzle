using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Interactables
{
    public class CraftingTable : MonoBehaviour, IInteractable
    {
        public List<Recipe.Recipe> recipe = new List<Recipe.Recipe>();
        public string InteractionName => "Press E to Craft Flashlight";

        public void Interact(PlayerInteraction pi)
        {
            if (pi.GetComponent<PlayerInventory>().Craft(recipe[0]))
            {
                pi.GetComponent<PlayerInventory>().AddInventoryItem(recipe[0].createdItem);
                pi.GetComponent<PlayerController>().ActivateFlashlight();
            }
            else
            {
                pi.GetComponent<PlayerUI>().DisplayAlert("Can't Create Item missing recipe items");
            }
        }
    }
}
