using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WBListType { whitelist, blacklist };

[CreateAssetMenuAttribute(fileName = "InventoryData", menuName = "Inventory/Inventory Data", order = 0)]
public class InventoryData : ScriptableObject {
    public new string name;
    public uint width, height;
    public WBListType wbListType;
    public List<ItemData> wbList;

    public InventoryData(string name, uint width, uint height, List<ItemData> wbList = null, WBListType wbListType = WBListType.whitelist)
    {
        this.name = name;
        this.width = width;
        this.height = height;
        this.wbListType = wbListType;
        this.wbList = wbList;
    }
}
