using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenuAttribute(fileName = "Inventory", menuName = "Inventory/Inventory", order = 1)]
public class InventoryData : ScriptableObject {

    public RuleTile ruleTile;
    public float paddingLeft, paddingRight, paddingTop, paddingBottom;
    public float edgeRadius;
    public bool grabAnywhere;
}
