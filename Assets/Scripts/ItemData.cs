using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class ItemData : ScriptableObject
{
    public static int SortByName(ItemData i1, ItemData i2)
    {
        return i1.name.CompareTo(i2.name);
    }

    public new string name;
    public Sprite sprite;

    // Bottom Left Corner is (0, 0)
    public List<Vector2Int> inventoryShape;
}
