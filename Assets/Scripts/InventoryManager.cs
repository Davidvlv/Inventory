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

    [SerializeField]
    List<Inventory> inventories = new List<Inventory>();

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
}
