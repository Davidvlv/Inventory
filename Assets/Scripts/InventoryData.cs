using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "InventoryData", menuName = "Inventory/Inventory Data", order = 0)]
public class InventoryData : ScriptableObject {
    public new string name;
    public uint width, height;
    public bool destroyOnClose;
}
