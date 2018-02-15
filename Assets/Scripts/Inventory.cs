using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// x ->, y down.
// 0, 0 is top left.
public class Inventory : MonoBehaviour {

    public Tilemap tilemap;
    public int height, width;

    private bool dragMouse;
    
    private Dictionary<Vector2Int, Item> itemGrid = new Dictionary<Vector2Int, Item>();

    private void Start()
    {
        //offset += new Vector2(transform.position.x, -transform.position.y);
    }

    /// <summary>
    /// Try to place an item at an inventory position (x increases upwards, y increases to the right)
    /// </summary>
    /// <param name="item">The item to be placed</param>
    /// <param name="position">The inventory position to place the item</param>
    /// <returns>Success of the operation</returns>
    public bool TryPlaceItem(Item item, Vector2Int position)
    {
        Debug.Log("try place " + item.data.name + " at " + position);
        if (!item.gameObject)
        {
            throw (new System.Exception("Invalid item, item must have a gameobject"));
        }

        // check if item fits
        foreach (Vector2Int itemPos in item.data.inventoryShape)
        {
            Vector2Int truePos = itemPos + position;
            
            // return false if we can't place the item
            if (truePos.x < 0 || truePos.y < 0 || truePos.x >= width || truePos.y >= height)
            {
                // item out of bounds
                return false;
            }

            Item itemAtPos;
            itemGrid.TryGetValue(truePos, out itemAtPos);
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
        foreach (Vector2Int itemPos in item.data.inventoryShape)
        {
            itemGrid.Add(position + itemPos, item);
        }

        return true;
    }

    public bool TryPlaceItem(Item item, Vector3 worldPosition, ref Vector2Int placedPosition)
    {
        Debug.Log("World Position: " + worldPosition);
        // convert World Position to inventory position
        Vector3 invPos = worldPosition - transform.position;

        Debug.Log("Inventory Position: " + invPos);

        Vector2Int invPosRounded = new Vector2Int((int)invPos.x, (int)invPos.y);

        Debug.Log("Rounded: " + invPosRounded);
        placedPosition = invPosRounded;

        return TryPlaceItem(item, invPosRounded);
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
