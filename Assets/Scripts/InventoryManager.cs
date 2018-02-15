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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Brings an inventory to the front of the screen
    /// </summary>
    /// <param name="inventory"></param>
    public void SendToFront(Inventory inventory)
    {
        inventories.Remove(inventory);
        inventories.Add(inventory);
    }

    public void AddInventory(Inventory inventory)
    {
        inventories.Add(inventory);
    }

    public Inventory TryPlaceItem(Item item, Vector3 worldPosition, ref Vector2Int placedPosition)
    {
        // try place item in each inventory (ordered front to back)
        foreach (Inventory inventory in inventories)
        {
            if (inventory.TryPlaceItem(item, worldPosition, ref placedPosition))
            {
                return inventory;
            }
        }
        return null;
    }
    public Inventory TryPlaceItem(Item item, Vector3 worldPosition)
    {
        Vector2Int unused = Vector2Int.zero;
        return TryPlaceItem(item, worldPosition, ref unused);
    }
}
