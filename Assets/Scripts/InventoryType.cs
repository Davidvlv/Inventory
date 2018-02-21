using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenuAttribute(fileName = "Inventory", menuName = "Inventory/Inventory", order = 1)]
public class InventoryType : ScriptableObject {

    public RuleTile ruleTile;
    public float paddingLeft, paddingRight, paddingTop, paddingBottom;
    public float edgeRadius;

    public bool closeable;
}
