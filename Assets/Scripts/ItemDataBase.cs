using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenuAttribute(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class ItemDataBase : ScriptableObject
{
    public static int SortByName(ItemDataBase i1, ItemDataBase i2)
    {
        return i1.name.CompareTo(i2.name);
    }

    public static int SortBySize(ItemDataBase i1, ItemDataBase i2)
    {
        return i2.inventoryShape.Count.CompareTo(i1.inventoryShape.Count);
    }

    public new string name;

    // Bottom Left Corner is (0, 0)
    public List<Vector2Int> inventoryShape;

    public virtual void InitializeItem(Item item)
    {

    }
}
