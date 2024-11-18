using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItemData> _inventoryItems = new List<InventoryItemData>();

    public void AddInventoryItem(InventoryItemData i)
    {
        _inventoryItems.Add(i);
    }
    public bool HasItem(string name)
    {
        foreach (var item in _inventoryItems)
        {
            if (item.Itemname == name)
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckDependency(List<string> items)
    {
        var items_len = items.Count;
        foreach (var item in _inventoryItems)
        {
            if (items.Contains(item.Itemname))
            {
                items_len--; 
            }
        }
        return items_len == 0;
    }
}
