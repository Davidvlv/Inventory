using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetup : MonoBehaviour {

    public Item testBoomerang;
    public Item testCoin;
    public Item testTorch;
    public Item testBag;
    public Item testSword;
    public Inventory createInventory;

    InventoryManager iManager;

    // Use this for initialization
    void Start()
    {
        iManager = InventoryManager.instance;
        
        iManager.TryPlaceItem(testBoomerang, new Vector3(-1.213f, -4.798f, 0));
        iManager.TryPlaceItem(testCoin, new Vector3(4.5f, 4.5f, 0));
        iManager.TryPlaceItem(testTorch, new Vector3(0, -4.5f, 0));
        iManager.TryPlaceItem(testSword, new Vector3(2, -2.5f, 0));
        //iManager.TryPlaceItem(testBag, new Vector3(3, -4.5f, 0));
        createInventory.TryPlaceItem(testBag, new Vector2Int(0, 2));
        
    }

    private void PlaceItem(Item item, Inventory inventory, int x, int y)
    {
        Debug.Log("Place " + item.name + " at " + x + ", " + y + " | success: " + inventory.TryPlaceItem(item, new Vector2Int(x,y)));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
