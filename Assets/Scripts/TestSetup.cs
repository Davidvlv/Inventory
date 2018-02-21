using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetup : MonoBehaviour {

    public List<ItemDataBase> items = new List<ItemDataBase>();

    InventoryManager iManager;

    // Use this for initialization
    void Start()
    {
        iManager = InventoryManager.instance;

        iManager.NewInventory(new Vector3(-12, 0), 2, 2);
        iManager.NewInventory(new Vector3(5, 0), 5, 4);
        iManager.NewInventoryWithItems(items);

        //iManager.TryPlaceItem(testBoomerang, new Vector3(-1.213f, -4.798f, 0));
        //iManager.TryPlaceItem(testCoin, new Vector3(4.5f, 4.5f, 0));
        //iManager.TryPlaceItem(testTorch, new Vector3(0, -4.5f, 0));
        //iManager.TryPlaceItem(testSword, new Vector3(2, -2.5f, 0));
        //iManager.TryPlaceItem(testBag, new Vector3(3, -4.5f, 0));
        //createInventory.TryPlaceItem(testBag, new Vector2Int(0, 0));

    }

    private void PlaceItem(Item item, Inventory inventory, int x, int y)
    {
        Debug.Log("Place " + item.name + " at " + x + ", " + y + " | success: " + inventory.TryPlaceItem(item, new Vector2Int(x,y)));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
