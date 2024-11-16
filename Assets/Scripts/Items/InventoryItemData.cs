using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewInventoryItemData", menuName = "Inventory/Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string Itemname;
    public string Description;
}
