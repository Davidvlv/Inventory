using UnityEngine;

public class MyInventory : MonoBehaviour {
    InventoryManager iManager;
    public InventoryData mainInventory;

    void Start () {
        iManager = InventoryManager.instance;

        // setup
        iManager.NewInventory(new Vector3(0, 0), mainInventory, false);
        //iManager.NewInventory(new Vector3(5, 0), 3, 3, "Another Inventory", null, false);
    }
	
	void Update () {
		
	}
}
