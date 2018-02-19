using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(TilemapRenderer))]
public class Inventory : MonoBehaviour
{
    public InventoryData data;
    public Tilemap tilemap;
    public TilemapRenderer tRenderer;
    public InventoryDrag topbar;
    BoxCollider2D box;
    private BoxCollider2D dragCollider;

    public uint height, width;

    private bool dragMouse;
    
    private Dictionary<Vector2Int, Item> itemGrid = new Dictionary<Vector2Int, Item>();
    
    private void Start()
    {
        // Create inventory UI
        CreateUI();
    }

    public void CreateUI()
    {
        tilemap.ClearAllTiles();
        for (int i = -1; i <= width; i++)
        {
            for (int j = -1; j <= height; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), data.ruleTile);
            }
        }

        // Set collider properties
        box = GetComponent<BoxCollider2D>();
        box.size = new Vector2(width + data.paddingLeft + data.paddingRight - data.edgeRadius * 2, 
                                height + data.paddingTop + data.paddingBottom - data.edgeRadius * 2);
        box.offset = new Vector2((width + data.paddingLeft - data.paddingRight) / 2, 
                                  (height + data.paddingTop - data.paddingBottom) / 2);
        box.edgeRadius = data.edgeRadius;

        // Set topbar collider properties
        topbar.topCollider.size = new Vector2(width + data.paddingLeft + data.paddingRight - data.edgeRadius * 2,
                                data.paddingTop - data.edgeRadius * 2);
        topbar.topCollider.offset = new Vector2((width + data.paddingLeft - data.paddingRight) / 2,
                                        height + data.paddingTop / 2);
        topbar.topCollider.edgeRadius = data.edgeRadius;
    }

    /// <summary>
    /// Try to place an item at an inventory position (x increases upwards, y increases to the right)
    /// </summary>
    /// <param name="item">The item to be placed</param>
    /// <param name="position">The inventory position to place the item</param>
    /// <returns>True: Success; False: Failed but in range; Null: out of range</returns>
    public bool TryPlaceItem(Item item, Vector2Int position)
    {
        if (!item.gameObject)
        {
            throw (new System.Exception("Invalid item, item must have a gameobject"));
        }
        Debug.Log("Trying to place item at: " + position);
        // check if item fits
        foreach (Vector2Int slotOffset in item.data.inventoryShape)
        {
            Debug.Log("Slot offset: " + slotOffset);
            Vector2Int slotPos = position + slotOffset;
            Debug.Log("Checking: " + slotPos);

            // return false if we can't place the item
            if (!InRange(slotPos))
            {
                Debug.Log("out of range");
                // item out of bounds
                return false;
            }
            Debug.Log("in range");
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
        Debug.Log("Inventory world position: " + worldPosition);
        placedPosition = WorldToInventoryPoint(worldPosition);
        Debug.Log("Inventory placed Position: " + placedPosition);
        return TryPlaceItem(item, placedPosition);
    }

    public Vector2Int WorldToInventoryPoint(Vector3 worldPoint)
    {
        Debug.Log("WTI worldPoint: " + worldPoint);
        Vector3 inventoryPoint = worldPoint - transform.position;
        Debug.Log("invP: " + worldPoint + " - " + transform.position + " = " + inventoryPoint);
        return new Vector2Int(Mathf.RoundToInt(inventoryPoint.x), Mathf.RoundToInt(inventoryPoint.y));
    }

    public bool InRange(Vector2Int slotPosition)
    {
        return (slotPosition.x >= 0 && slotPosition.y >= 0 && slotPosition.x < width && slotPosition.y < height);
    }

    public bool InRange(Vector3 worldPoint)
    {
        return InRange(WorldToInventoryPoint(worldPoint));
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

    public void SetLayer(int x)
    {
        Vector3 temp = transform.position;
        temp.z = -x;
        transform.position = temp;
    }
}
