using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class ItemData : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public List<Vector2Int> inventoryShape;
}
