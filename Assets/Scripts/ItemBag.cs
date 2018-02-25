using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : Item {
    InventoryManager iManager;

    public Inventory childInventory;

    public void Awake()
    {
        iManager = InventoryManager.instance;
    }

    public override void Interact()
    {
        base.Interact();
        placedInventory.RemoveItem(rootPos, true);
        Debug.Log("I'm a bag!!!!!");
        //iManager.
    }
}
