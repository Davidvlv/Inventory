using System;
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
    internal void Initialize(Sprite button, Sprite buttonDown)
    {
        sprite = button;
        spriteDown = buttonDown;
        spriteRenderer.sprite = sprite;
    }

    private void OnMouseDown()
    {
        iManager.SendToFront(inventory);
        spriteRenderer.sprite = spriteDown;
    }

    private void OnMouseUp()
    {
        spriteRenderer.sprite = sprite;
        iManager.Close(inventory);
    }

}
