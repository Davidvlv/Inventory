using UnityEngine;

public class MyInventory : MonoBehaviour {
    InventoryManager iManager;


    void Start () {
        iManager = InventoryManager.instance;

        // setup
        iManager.NewInventory(new Vector3(0, 0), 6, 4, "Main Inventory", null, false);
        //iManager.NewInventory(new Vector3(5, 0), 3, 3, "Another Inventory", null, false);
    }
	
	void Update () {
		
	}
}
