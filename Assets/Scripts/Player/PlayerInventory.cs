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
    public void RemoveInventoryItem(string s)
    {
        foreach (var item in _inventoryItems)
        {
            if (item.Itemname == s)
            {
                _inventoryItems.Remove(item);
                break;
            }
        }
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
    private bool CheckDependency(List<string> items)
    {
        int ctr = 0;
        foreach (var item in items)
        {
            Debug.Log(item);
            if (HasItem(item))
            {
                ctr++;
            }
        }
        return ctr == items.Count - 1;
    }
    public bool Craft(List<string> recipe)
    {
        if (CheckDependency(recipe)){
            foreach (var item in recipe)
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
