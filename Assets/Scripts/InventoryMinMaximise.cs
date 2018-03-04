using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMinMaximise : MonoBehaviour
{
    public Inventory inventory;
    public SpriteRenderer spriteRenderer;

    private InventoryManager iManager;

    public Sprite spriteMin;
    public Sprite spriteMinDown;
    public Sprite spriteMax;
    public Sprite spriteMaxDown;

    private bool isMin = true;

    private void Start()
    {
        iManager = InventoryManager.instance;
    }

    internal void Initialize(Sprite minSprite, Sprite minSpriteDown, Sprite maxSprite, Sprite maxSpriteDown)
    {
        spriteMin = minSprite;
        spriteMinDown = minSpriteDown;
        spriteMax = maxSprite;
        spriteMaxDown = maxSpriteDown;
        spriteRenderer.sprite = spriteMin;
    }

    private void OnMouseDown()
    {
        iManager.SendToFront(inventory);
        spriteRenderer.sprite = isMin ? spriteMinDown : spriteMaxDown;
    }

    private void OnMouseUp()
    { 
        // minimise/maximise
        Set(!isMin);
    }

    public void Set(bool isMin)
    {
        this.isMin = isMin;
        spriteRenderer.sprite = isMin ? spriteMin : spriteMax;
        inventory.MinMaximise(isMin);
    }
}
