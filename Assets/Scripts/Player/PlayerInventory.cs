using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryItemData> _inventoryItems;

    public void AddInventoryItem(InventoryItemData i)
    {
        _inventoryItems.Add(i);
    }
}
