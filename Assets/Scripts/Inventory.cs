using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(TilemapRenderer))]
public class Inventory : MonoBehaviour
{
    public InventoryType type;
    public Tilemap tilemap;
    public TilemapRenderer tRenderer;
    public InventoryDrag topbar;
    BoxCollider2D box;
    public InventoryClose closeButton;

    public uint height, width;

    //public bool closeOnEmpty { get; private set; }
    public bool destroyOnClose { get; private set; }
    
    private Dictionary<Vector2Int, Item> itemGrid = new Dictionary<Vector2Int, Item>();

    internal void Initialize(InventoryType type, uint height, uint width, string name, bool destroyOnClose)
    {
        this.type = type;
        this.height = height;
        this.width = width;
        this.name = name;
        //this.closeOnEmpty = closeOnEmpty;
        //this.destroyOnClose = closeOnEmpty ? true : destroyOnClose; // if it's closeOnEmpty, then it must be destroyOnClose
        this.destroyOnClose = destroyOnClose;

        this.closeButton.sprite = type.closeButton;
        this.closeButton.spriteDown = type.closeButtonDown;
    }

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
                tilemap.SetTile(new Vector3Int(i, j, 0), type.ruleTile);
            }
        }

        // Set collider properties
        box = GetComponent<BoxCollider2D>();
        box.size = new Vector2(width + type.paddingLeft + type.paddingRight - type.edgeRadius * 2, 
                                height + type.paddingTop + type.paddingBottom - type.edgeRadius * 2);
        box.offset = new Vector2((width + type.paddingLeft - type.paddingRight) / 2, 
                                  (height + type.paddingTop - type.paddingBottom) / 2);
        box.edgeRadius = type.edgeRadius;

        // Set topbar collider properties
        topbar.topCollider.size = new Vector2(width + type.paddingLeft + type.paddingRight - type.edgeRadius * 2,
                                type.paddingTop - type.edgeRadius * 2);
        topbar.topCollider.offset = new Vector2((width + type.paddingLeft - type.paddingRight) / 2,
                                        height + type.paddingTop / 2);
        topbar.topCollider.edgeRadius = type.edgeRadius;

        // close button
        closeButton.transform.localPosition = new Vector3(width - type.closeButtonPaddingRight, height - type.closeButtonPaddingTop, -0.2f);
    }

    public void Resize(uint width, uint height)
    {
        this.width = width;
        this.height = height;
        CreateUI();
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
        // check if item fits
        foreach (Vector2Int slotOffset in item.data.slotPositions)
        {
            Vector2Int slotPos = position + slotOffset;

            // return false if we can't place the item
            if (!InRange(slotPos))
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
        foreach (Vector2Int slotOffset in item.data.slotPositions)
        {
            itemGrid.Add(position + slotOffset, item);
        }
        return true;
    }

    public bool TryPlaceItem(Item item, Vector3 worldPosition, ref Vector2Int placedPosition)
    {
        placedPosition = WorldToInventoryPoint(worldPosition);
        return TryPlaceItem(item, placedPosition);
    }

    public Vector2Int WorldToInventoryPoint(Vector3 worldPoint)
    {
        Vector3 inventoryPoint = worldPoint - transform.position;
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

        foreach (Vector2Int itemPos in item.data.slotPositions)
        {
            itemGrid.Remove(position + itemPos);
        }

        if (destroyItem)
        {
            Destroy(item.gameObject);
        }

        //if (itemGrid.Count == 0 && closeOnEmpty)
        //{
        //    InventoryManager.instance.DestroyInventory(this);
        //}

    }

    // needs to route to above function
    //public void RemoveItem(Item item)
    //{
    //    if (!ContainsItem(item))
    //    {
    //        return;
    //    }

    //    if (!itemGrid.ContainsKey(item.rootPos))
    //    {
    //        throw (new System.Exception("Item rootPos does not align with the inventory position"));
    //    }

    //    itemGrid.Remove(item.rootPos);
    //}

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
