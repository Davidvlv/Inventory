using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Inventory : MonoBehaviour {

    private Tilemap tilemap;
    public int height, width;

    private bool dragMouse;
    
    private Dictionary<Vector2Int, Item> itemGrid = new Dictionary<Vector2Int, Item>();

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    /// <summary>
    /// Try to place an item at an inventory position (x increases upwards, y increases to the right)
    /// </summary>
    /// <param name="item">The item to be placed</param>
    /// <param name="position">The inventory position to place the item</param>
    /// <returns>Success of the operation</returns>
    public bool TryPlaceItem(Item item, Vector2Int position)
    {
        if (!item.gameObject)
        {
            throw (new System.Exception("Invalid item, item must have a gameobject"));
        }

        // check if item fits
        foreach (Vector2Int slotOffset in item.data.inventoryShape)
        {
            Vector2Int slotPos = position + slotOffset;
            
            // return false if we can't place the item
            if (slotPos.x < 0 || slotPos.y < 0 || slotPos.x >= width || slotPos.y >= height)
            {
                // item out of bounds
                return false;
            }

            Item itemAtPos;
            itemGrid.TryGetValue(slotPos, out itemAtPos);
            
            if (itemAtPos != null)
            {
                if (itemAtPos.gameObject != item.gameObject)
                {
                    // item slot already taken
                    return false;
                }
            }
        }
        
        // place item - all item positions are clear
        item.Place(this, position);

        // map each position in the grid to the item
        foreach (Vector2Int slotOffset in item.data.inventoryShape)
        {
            itemGrid.Add(position + slotOffset, item);
        }
        
        // debug each slot
        foreach(Vector2Int key in itemGrid.Keys)
        {
            Item value;
            itemGrid.TryGetValue(key, out value);
        }
        return true;
    }

    public bool TryPlaceItem(Item item, Vector3 worldPosition, ref Vector2Int placedPosition)
    {
        // convert World Position to inventory position
        Vector3 inventoryPosition = worldPosition - transform.position;

        // round
        placedPosition = new Vector2Int((int)inventoryPosition.x, (int)inventoryPosition.y);
        
        return TryPlaceItem(item, placedPosition);
    }

    public void RemoveItem(Vector2Int position, bool destroyItem = false)
    {
        Item item;
        itemGrid.TryGetValue(position, out item);

        if (item == null)
        {
            return;
        }

        foreach (Vector2Int itemPos in item.data.inventoryShape)
        {
            itemGrid.Remove(position + itemPos);
        }

        if (destroyItem)
        {
            Destroy(item.gameObject);
        }
    }

    public void RemoveItem(Item item)
    {
        if (!ContainsItem(item))
        {
            return;
        }

        if (!itemGrid.ContainsKey(item.rootPos))
        {
            throw (new System.Exception("Item rootPos does not align with the inventory position"));
        }

        itemGrid.Remove(item.rootPos);
    }

    public bool ContainsItem(Item item)
    {
        return itemGrid.ContainsValue(item);
    }

    public void RemoveItem(int x, int y)
    {
        RemoveItem(new Vector2Int(x, y));
    }
}
