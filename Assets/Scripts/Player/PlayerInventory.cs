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
    public void RemoveInventoryItem(InventoryItemData item)
    {
        foreach (var it in _inventoryItems)
        {
            if (item.Itemname == it.Itemname)
            {
                _inventoryItems.Remove(item);
                break;
            }
        }
    }
    public bool HasItem(string itemname)
    {
        foreach (var item in _inventoryItems)
        {
            if (itemname == item.Itemname)
            {
                return true;
            }
        }
        return false;
    }
    public bool HasItem(InventoryItemData item)
    {
        foreach (var it in _inventoryItems)
        {
            if (item.Itemname == it.Itemname)
            {
                return true;
            }
        }
        return false;
    }
    private bool CheckDependency(Recipe r)
    {
        int ctr = 0;
        foreach (var item in r.Items)
        {
            if (HasItem(item))
            {
                ctr++;
            }
        }
        return ctr == r.Items.Count;
    }
    public bool Craft(Recipe r)
    {
        if (CheckDependency(r))
        {
            foreach (var item in r.Items)
            {
                if (HasItem(item))
                {
                    RemoveInventoryItem(item);
                }
            }
            return true;
        }
        return false;
    }
}
