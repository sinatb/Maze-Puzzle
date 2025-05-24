using Player;
using UnityEngine;

namespace Items
{
    public class InventoryItem : MonoBehaviour, IInteractable
    {
        public InventoryItemData data;
        public string InteractionName => "Press E to Pick Up " + data.itemName;

        public void Interact(PlayerInteraction pi)
        {
            pi.GetComponent<PlayerInventory>().AddInventoryItem(data);
            gameObject.SetActive(false);
        }
    }
}
