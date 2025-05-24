using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Recipe
{
    [CreateAssetMenu(fileName = "Recipe", menuName ="Inventory/Recipe")]
    public class Recipe : ScriptableObject
    {
        public List<InventoryItemData> items; 
        public InventoryItemData createdItem;
    }
}
