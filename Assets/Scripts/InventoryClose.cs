using UnityEngine;

public class InventoryClose : MonoBehaviour
{
    public Inventory inventory;
    public SpriteRenderer spriteRenderer;

    private InventoryManager iManager;

    public Sprite sprite;
    public Sprite spriteDown;

    private void Start()
    {
        iManager = InventoryManager.instance;
    }

    private void OnMouseDown()
    {
        spriteRenderer.sprite = spriteDown;
    }

    private void OnMouseUp()
    {
        spriteRenderer.sprite = sprite;
        iManager.Close(inventory);
    }
}
