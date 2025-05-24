using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        private Dictionary<InventoryItemData, int> _inventoryItems = new Dictionary<InventoryItemData, int>();

        public void AddInventoryItem(InventoryItemData i)
        {
            _inventoryItems[i]++;
        }
        public void RemoveInventoryItem(InventoryItemData item)
        {
            if (_inventoryItems.Any(it => item.itemName == it.Key.itemName))
            {
                _inventoryItems.Remove(item);
            }
        }
        public bool HasItem(string itemName)
        {
            return _inventoryItems.Any(item => itemName == item.Key.itemName);
        }
        public bool HasItem(InventoryItemData item)
        {
            return _inventoryItems.Any(it => item.itemName == it.Key.itemName);
        }
        private bool CheckDependency(Recipe.Recipe r)
        {
            var ctr = r.items.Count(HasItem);
            return ctr == r.items.Count;
        }
        public bool Craft(Recipe.Recipe r)
        {
            if (!CheckDependency(r)) return false;
            foreach (var item in r.items.Where(HasItem))
            {
                RemoveInventoryItem(item);
            }
            return true;
        }
    }
}
