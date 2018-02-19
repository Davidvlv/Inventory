using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    private GameObject ItemUIClickPrefab;

    public ItemData data;

    public Vector2Int rootPos;

    public SpriteRenderer spriteRenderer;

    public Inventory inventory;

    [SerializeField]
    private bool initialized;

    private static Vector3 offset = new Vector3(0.5f, 0.5f, 0);

    private void Start()
    {
        if (!initialized)
        {
            throw (new System.Exception("Item not initialized! Item must be initialized on Instantiation"));
        }

        // Make grab points
        foreach(Vector2Int position in data.inventoryShape)
        {
            GameObject UIClicker = Instantiate(ItemUIClickPrefab, transform);
            ItemSlot slot = UIClicker.GetComponent<ItemSlot>();
            slot.Initialize(this, position);

        }
    }

    public void Initialize(ItemData data, Vector2Int rootPos, Inventory inventory)
    {
        initialized = true;

        this.data = data;
        spriteRenderer.sprite = data.sprite;

        this.rootPos = rootPos;
        this.inventory = inventory;
    }

    public void RemoveFromInventory()
    {
        inventory.RemoveItem(rootPos);
    }

    public void Place(Inventory inventory, Vector2Int position)
    {
        if (this.inventory)
        {
            RemoveFromInventory();
        }

        // check if we are placing in a different inventory
        if (this.inventory.gameObject != inventory.gameObject)
        {
            // swap inventories
            this.inventory = inventory;
        }
        rootPos = position;

        transform.parent = inventory.transform;
        SetLocalPosition();
    }

    public void SetLocalPosition()
    {
        transform.localPosition = offset + new Vector3(rootPos.x, rootPos.y, -0.5f);
    }
}
