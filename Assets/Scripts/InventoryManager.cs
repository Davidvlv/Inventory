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
        Destroy(inventory.gameObject);
    }

    public void Close(Inventory inventory)
    {
        if (inventory.destroyOnClose)
        {
            DestroyInventory(inventory);
            return;
        }

        // hide the inventory
        inventory.gameObject.SetActive(false);
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

    public Inventory NewInventory(Vector3 worldPosition, uint width, uint height, string name = "Inventory", InventoryType type = null, bool destroyOnClose = true)
    {
        if (!type)
        {
            type = defaultType;
        }

        GameObject obj = Instantiate(inventoryPrefab, transform);
        obj.transform.position = new Vector3(worldPosition.x - width/2, worldPosition.y - height/2);
        Inventory inventory = obj.GetComponent<Inventory>();
        inventory.Initialize(type, width, height, name, destroyOnClose);

        AddInventory(inventory);

        return inventory;
    }

    public void NewInventoryWithItems(List<ItemDataBase> items, string name = "Inventory", InventoryType type = null)
    {
        if (items.Count == 0)
        {
            return;
        }

        bool incrementHeightOrWidth = false;
        uint height = 1, width = 1;
        Inventory newInventory = NewInventory(Vector3.zero, width, height, name, type);

        // biggest to smallest for best packing
        items.Sort(ItemDataBase.SortBySize);

        foreach (ItemDataBase data in items)
        {
            Item item = Instantiate(itemPrefab).GetComponent<Item>();
            item.Initialize(data, new Vector2Int(0, 0));

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
    }

    private bool PackItem(Item item, Inventory inventory)
    {
        bool placed = false;
        for (int i = 0; i < inventory.width; i++)
        {
            for (int j = 0; j < inventory.height; j++)
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
