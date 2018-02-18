using UnityEngine;

public class InventoryDrag : MonoBehaviour {
    public Inventory inventory;
    public BoxCollider2D topCollider;

    private Vector3 offset;

    private Camera cam;

    private bool isDragged;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        isDragged = true;
        offset = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
        offset.z = 0;
    }

    private void Update()
    {
        if (!isDragged)
            return;

        //stick item to mouse
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)); // need to specify z for some reason
        mousePos.z = inventory.transform.position.z;
        inventory.transform.position = mousePos + offset;

    }

    private void OnMouseUp()
    {
        isDragged = false;
    }
}
