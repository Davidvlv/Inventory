using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour {
    public Item item;
    public Vector2Int offset;

    private Camera cam;

    private bool isDragged;

    private void Start()
    {
        cam = Camera.main;
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
        Vector3 mousePos = Input.mousePosition + new Vector3(0, 0, 10); // need to specify z for some reason
        mousePos = cam.ScreenToWorldPoint(mousePos);
        mousePos.x -= offset.x;
        mousePos.y += offset.y;
        item.transform.position = mousePos;

    }

    private void OnMouseUp()
    {
        isDragged = false;

        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 invPos = new Vector3(worldPosition.x + item.inventory.width / 2 - offset.x,
                                    -worldPosition.y + item.inventory.height / 2 - offset.y, 0);
        Vector2Int intPos = new Vector2Int((int)invPos.x, (int)invPos.y);
        
        bool moveSucceed = item.inventory.TryPlaceItem(item, intPos);

        Vector3 truePos = item.GetWorldPosition();
        item.transform.position = truePos;
    }
}
