using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour {
    private Item item;
    private Vector3 offset;
    private static Vector3 halfSquare = new Vector3(0.5f, 0.5f, 0);
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
        this.offset = new Vector3(offset.x, offset.y, 0);// + halfSquare;
        transform.Translate(this.offset + halfSquare);
    }

    private void OnMouseDown()
    {
        isDragged = true;
    }

    private void Update()
    {
        if (!isDragged)
            return;

        // stick item to mouse
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)); // need to specify z for some reason
        mousePos.z = -0.5f;
        item.transform.position = mousePos - offset;

        // move item in front of all inventories
        //Vector3 temp = item.transform.localPosition;
        //temp.z = -0.5f;
        //item.transform.localPosition = temp;

    }

    private void OnMouseUp()
    {
        isDragged = false;
        Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Debug.Log("Offset : " + offset);
        Debug.Log("ItemSlot position" + (cam.ScreenToWorldPoint(Input.mousePosition) - offset));
        Vector2Int placedPosition = Vector2Int.zero;
        // Get the world position
        Inventory placedInventory = iManager.TryPlaceItem(item, cam.ScreenToWorldPoint(Input.mousePosition) - offset - halfSquare, ref placedPosition);

        if (!placedInventory)
        {
            // reset position
            item.SetLocalPosition();
            return;
        }
    }
}
