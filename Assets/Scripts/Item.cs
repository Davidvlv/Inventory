using UnityEngine;

public class Item : MonoBehaviour {
    [SerializeField]
    private GameObject ItemUIClickPrefab;

    public ItemData data;

    public Vector2Int rootPos;

    public SpriteRenderer spriteRenderer;

    public Inventory inventory;

    private void Start()
    {
        foreach(Vector2Int position in data.inventoryShape)
        {
            GameObject UIClicker = Instantiate(ItemUIClickPrefab, transform);
            UIClicker.transform.Translate(new Vector3(position.x, -position.y, 0));
            ItemUI ui = UIClicker.GetComponent<ItemUI>();
            ui.item = this;
            ui.offset = position;

        }
    }

    public void Initialize(ItemData data, Vector2Int rootPos, Inventory inventory)
    {
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
        RemoveFromInventory();

        // check if we are placing in a different inventory
        if (this.inventory.gameObject != inventory.gameObject)
        {
            // swap inventories
            this.inventory = inventory;
        }
        rootPos = position;

        transform.position = new Vector3(position.x - inventory.width * 0.5f + inventory.offset.x, 
                                        -position.y + inventory.height * 0.5f - inventory.offset.y, 5);
    }

    public void BeginDrag()
    {
        Debug.Log("Begin Drag");

    }

    public Vector3 GetWorldPosition()
    {
        return new Vector3(rootPos.x - inventory.width * 0.5f + inventory.offset.x,
                          -rootPos.y + inventory.height * 0.5f - inventory.offset.y, 5);
    }
}
