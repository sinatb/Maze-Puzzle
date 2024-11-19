using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName ="Inventory/Recipe")]
public class Recipe : ScriptableObject
{
    public List<InventoryItemData> Items;
    public InventoryItemData CreatedItem;
}
