using UnityEngine;

public class Item : MonoBehaviour {

    public GameObject itemUIClickPrefab;

    public ItemDataBase data;

    public Vector2Int rootPos;

    //public SpriteRenderer spriteRenderer;

    public Inventory placedInventory;

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
            GameObject UIClicker = Instantiate(itemUIClickPrefab, transform);
            ItemSlot slot = UIClicker.GetComponent<ItemSlot>();
            slot.Initialize(this, position);

        }
    }

    public void Initialize(ItemDataBase data, Vector2Int rootPos, Inventory placedInventory = null)
    {
        initialized = true;

        this.data = data;
        name = data.name;
        data.InitializeItem(this);

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
}
