using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : Item {

    public Inventory childInventory;

    public override void Interact()
    {
        base.Interact();

        Debug.Log("I'M A BAG");
    }
}
