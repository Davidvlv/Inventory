using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour {
    private Item item;
    private Vector3 offset;

    private Camera cam;

    private bool isDragged;

    private InventoryManager iManager;

    private void Start()
    {
        cam = Camera.main;
        iManager = InventoryManager.instance;
    }

    public void Initialize(Item item, Vector2Int offset)
    {
        this.item = item;
        this.offset = new Vector3(offset.x, offset.y, 0);
    }

    private void OnMouseDown()
    {
        isDragged = true;
    }

    private void Update()
    {
        if (!isDragged)
            return;

        //stick item to mouse
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)); // need to specify z for some reason
        mousePos.z = 3;
        item.transform.position = mousePos - offset;

    }

    private void OnMouseUp()
    {
        isDragged = false;

        Vector2Int placedPosition = Vector2Int.zero;
        // Get the world position
        Inventory placedInventory = iManager.TryPlaceItem(item, cam.ScreenToWorldPoint(Input.mousePosition) - offset, ref placedPosition);

        if (!placedInventory)
        {
            // reset position
            item.SetLocalPosition();
            return;
        }
    }
}
