﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Singleton

    public static InventoryManager instance;

    private void Awake()
    {
        if (instance)
        {
            throw (new System.Exception("Singleton InventoryManager has more than one instance"));
        }
        instance = this;
    }

    #endregion

    public GameObject inventoryPrefab;

    public InventoryType defaultType;

    [SerializeField]
    List<Inventory> inventories = new List<Inventory>();
    // #TODO close inventories

    private void Update()
    {
        // temp show all hidden inventories
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach(Inventory inventory in inventories)
            {
                inventory.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Brings an inventory to the front of the screen
    /// </summary>
    /// <param name="inventory"></param>
    public void SendToFront(Inventory inventory)
    {
        inventories.Remove(inventory);
        inventories.Add(inventory);
        OrderZ();
    }

    public void OrderZ()
    {
        if (inventories.Count <= 1)
        {
            return;
        }
        // places inventories at z= 0, 1, 2, ...
        for (int i = 0; i < inventories.Count; i++)
        {
            //Debug.Log(inventories.Count + " - 1 - " + i + " = " + (inventories.Count - 1 - i));
            Vector3 temp = inventories[inventories.Count - 1 - i].transform.position;
            temp.z = i;
            inventories[inventories.Count - 1 - i].transform.position = temp;
        }
    }

    public void AddInventory(Inventory inventory, bool closeable = true)
    {
        // can't add an inventory twice
        if (inventories.Contains(inventory))
        {
            return;
        }

        inventories.Add(inventory);
        OrderZ();
    }

    public void DestroyInventory(Inventory inventory)
    {
        if (!inventories.Contains(inventory))
        {
            throw (new System.Exception("Trying to destroy an inventory that isn't listed in the Inventory Manager - how did this happen?"));
        }
        inventories.Remove(inventory);
        inventory.Destroy();
    }

    public void Close(Inventory inventory)
    {
        if (!inventory.isPermanent)
        {
            DestroyInventory(inventory);
            return;
        }

        // hide the inventory
        inventory.gameObject.SetActive(false);
    }

    public void Open(Inventory inventory)
    {
        if (!inventories.Contains(inventory))
        {
            throw (new System.Exception("Trying to open an inventory that isn't listed in the Inventory Manager - how did this happen?"));
        }

        inventory.gameObject.SetActive(true);
        SendToFront(inventory);
    }

    public Inventory TryPlaceItem(Item item, Vector3 worldPosition, ref Vector2Int placedPosition)
    {
        // try place item in each inventory (ordered front to back)
        for (int i = inventories.Count - 1; i >= 0; i--)
        {
            if (!inventories[i].isActiveAndEnabled)
            {
                continue;
            }
            if (!inventories[i].InRange(worldPosition))
            {
                continue;
            }
            if (!inventories[i].TryPlaceItem(item, worldPosition, ref placedPosition))
            {
                return null;
            }
            return inventories[i];
        }
        return null;
    }
    public Inventory TryPlaceItem(Item item, Vector3 worldPosition)
    {
        Vector2Int unused = Vector2Int.zero;
        return TryPlaceItem(item, worldPosition, ref unused);
    }

    public Inventory NewInventory(Vector3 worldPosition, InventoryData data, bool isPermanent = false, InventoryType type = null)
    {
        if (!type)
        {
            type = defaultType;
        }
        if (!data)
        {
            data = ScriptableObject.CreateInstance<InventoryData>();
            data.name = "New";
            data.height = 1;
            data.width = 1;
        }

        GameObject obj = Instantiate(inventoryPrefab, transform);
        obj.transform.position = new Vector3(worldPosition.x - data.width/2, worldPosition.y - data.height/2);
        Inventory inventory = obj.GetComponent<Inventory>();
        inventory.Initialize(type, data, isPermanent);

        AddInventory(inventory);

        return inventory;
    }

    public Inventory NewInventoryWithItems(List<ItemData> items, InventoryData data = null, InventoryType type = null)
    {
        // clone the list as to not change it
        List<ItemData> createItems = new List<ItemData>(items);
        if (items.Count == 0)
        {
            return null;
        }

        bool incrementHeightOrWidth = false;
        uint height = 1, width = 1;
        Inventory newInventory = NewInventory(Vector3.zero, data, false, type);

        // biggest to smallest for best packing
        createItems.Sort(ItemData.SortBySize);

        foreach (ItemData itemData in createItems)
        {
            GameObject itemObject = new GameObject(itemData.name);
            Item item = itemData.CreateItem(itemObject);
            item.Initialize(itemData, new Vector2Int(0, 0));

            // try place item in each slot
            bool placed = false;
            while (!placed)
            {
                placed = PackItem(item, newInventory);
                if (!placed)
                {
                    if (incrementHeightOrWidth)
                    {
                        height++;
                    }
                    else
                    {
                        width++;
                    }
                    incrementHeightOrWidth = !incrementHeightOrWidth;

                    newInventory.Resize(height, width);
                }
                if (width > 10)
                {
                    break;
                }
            }

        }

        return newInventory;
    }

    public Inventory NewInventoryWithItems(List<ItemData> items, string name, uint width, uint height, 
        WBListType wbType = WBListType.whitelist, List<ItemData> wbList = null, InventoryType type = null)
    {
        InventoryData data = ScriptableObject.CreateInstance<InventoryData>();
        data.name = name;
        data.width = 1;
        data.height = 1;
        data.wbType = wbType;
        data.wbList = wbList;

        return NewInventoryWithItems(items, data, type);
    }

    private bool PackItem(Item item, Inventory inventory)
    {
        if (!inventory.CanHold(item))
        {
            return false;
        }

        bool placed = false;
        for (int i = 0; i < inventory.data.height; i++)
        {
            for (int j = 0; j < inventory.data.width; j++)
            {
                placed = inventory.TryPlaceItem(item, new Vector2Int(i, j));
                if (placed)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
