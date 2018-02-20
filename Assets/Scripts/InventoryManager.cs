using System.Collections;
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
    public GameObject itemPrefab;
    public InventoryType defaultType;

    [SerializeField]
    List<Inventory> inventories = new List<Inventory>();
    // #TODO close inventories

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
        // places inventories at z= 0, 1, 2, ...
        for (int i = 0; i < inventories.Count; i++)
        {
            Vector3 temp = inventories[inventories.Count - 1 - i].transform.position;
            temp.z = i;
            inventories[inventories.Count - 1 - i].transform.position = temp;
        }
    }

    public void AddInventory(Inventory inventory)
    {
        inventories.Add(inventory);
        OrderZ();
    }

    public void NewInventoryWithItems(List<ItemData> items)
    {
        uint height = 1, width = 1;
        Inventory newInventory = NewInventory(Vector3.zero, width, height);
        foreach (ItemData data in items)
        {
            Item item = Instantiate(itemPrefab).GetComponent<Item>();
            item.Initialize(data, new Vector2Int(0, 0), newInventory);

            // try place item in each slot
            bool placed = false;
            while (!placed)
            {
                for (int i = 0; i < newInventory.width; i++)
                {
                    for (int j = 0; j < newInventory.height; j++)
                    {
                        placed = newInventory.TryPlaceItem(item, new Vector2Int(i, j));
                    }
                }
                if (!placed)
                {
                    newInventory.Resize(++height, ++width);
                }
                if (width > 5)
                {
                    break;
                }
            }

        }
    }

    public Inventory TryPlaceItem(Item item, Vector3 worldPosition, ref Vector2Int placedPosition)
    {
        // try place item in each inventory (ordered front to back)
        for (int i = inventories.Count - 1; i >= 0; i--)
        {
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="worldPosition">The world position of the middle of the new inventory</param>
    /// <param name="inventory">The inventory type to </param>
    /// <returns></returns>
    public Inventory NewInventory(Vector3 worldPosition, uint width, uint height, string name = "Inventory", InventoryType type = null)
    {
        if (!type)
        {
            type = defaultType;
        }

        GameObject obj = Instantiate(inventoryPrefab, transform);
        obj.transform.position = new Vector3(worldPosition.x - width/2, worldPosition.y - height/2);
        Inventory inventory = obj.GetComponent<Inventory>();
        inventory.Initialize(type, width, height, name);

        AddInventory(inventory);

        return inventory;
    }
}
