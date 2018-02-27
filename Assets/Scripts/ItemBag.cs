using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : Item {
    InventoryManager iManager;

    public Inventory bagInventory;

    private void Awake()
    {
        iManager = InventoryManager.instance;
    }

    public void Initialize(Inventory bagInventory, ItemData data, Vector2Int rootPos, Inventory placedInventory = null)
    {
        this.bagInventory = bagInventory;
        Initialize(data, rootPos, placedInventory);
    }

    public override void Initialize(ItemData data, Vector2Int rootPos, Inventory placedInventory = null)
    {
        base.Initialize(data, rootPos, placedInventory);
        if (!bagInventory)
        {
            bagInventory = iManager.NewInventory(Vector3.zero, data.bagInventoryData);
            bagInventory.gameObject.SetActive(false);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    public override bool Place(Inventory inventory, Vector2Int position)
    {
        // can't place inside its own inventory
        if (bagInventory.ContainsInventory(inventory))
        {
            return false;
        }
        if (!inventory.CanHold(this))
        {
            return false;
        }
        
        // place me
        if (!base.Place(inventory, position))
        {
            return false;
        }

        if (initialized)
        {
            // propogate destroyOnClose to all children
            PropogateDestroyOnClose(inventory.destroyOnClose);
        }

        return true;
    }

    public void PropogateDestroyOnClose(bool destroyOnClose)
    {
        bagInventory.PropogateDestroyOnClose(destroyOnClose);
    }

    public override void Interact()
    {
        base.Interact();

        iManager.Open(bagInventory);
    }

    public override void Destroy()
    {
        iManager.DestroyInventory(bagInventory);

        base.Destroy();
    }
}
