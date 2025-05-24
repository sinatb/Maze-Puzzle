using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName ="NewInventoryItemData", menuName = "Inventory/Item Data")]
    public class InventoryItemData : ScriptableObject
    {
        public string itemName;
        public string description;
    }
}
