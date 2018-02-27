using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WBListType { whitelist, blacklist };

[CreateAssetMenuAttribute(fileName = "InventoryData", menuName = "Inventory/Inventory Data", order = 0)]
public class InventoryData : ScriptableObject {
    public new string name;
    public uint width, height;
    public WBListType wbType;
    public List<ItemData> wbList;
}
