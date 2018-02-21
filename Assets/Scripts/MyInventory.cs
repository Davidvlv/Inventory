using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInventory : MonoBehaviour {
    InventoryManager iManager;

    public InventoryType mainInventory;
    
	void Start () {
        iManager = InventoryManager.instance;

        // setup
        iManager.NewInventory(new Vector3(0, 0), 6, 4, "My Backpack", mainInventory);
	}
	
	void Update () {
		
	}
}
