using UnityEngine;

public class Item : MonoBehaviour {

    public ItemData data;

    public Vector2Int rootPos;

    //public SpriteRenderer spriteRenderer;

    public Inventory placedInventory;

    [SerializeField]
    private bool initialized;

    private static Vector3 offset = new Vector3(0.5f, 0.5f, 0);

    protected virtual void Start()
    {
        if (!initialized)
        {
            throw (new System.Exception("Item not initialized! Item must be initialized on Instantiation"));
        }

        // Make grab points
        foreach(Vector2Int slotPosition in data.slotPositions)
        {
            GameObject UIClicker = new GameObject("Item Slot");
            ItemSlot slot = UIClicker.AddComponent<ItemSlot>();
            slot.Initialize(this, slotPosition);
        }
    }

    public virtual void Initialize(ItemData data, Vector2Int rootPos, Inventory placedInventory = null)
    {
        initialized = true;

        this.data = data;
        name = data.name;

        this.rootPos = rootPos;
        this.placedInventory = placedInventory;
    }

    public void RemoveFromInventory()
    {
        placedInventory.RemoveItem(rootPos);
    }

    public void Place(Inventory inventory, Vector2Int position)
    {
        if (this.placedInventory)
        {
            RemoveFromInventory();
        }

        rootPos = position;

        this.placedInventory = inventory;

        transform.parent = inventory.transform;
        SetLocalPosition();
    }

    public void SetLocalPosition()
    {
        transform.localPosition = offset + new Vector3(rootPos.x, rootPos.y, -0.5f);
    }

    public virtual void Interact()
    {

    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
