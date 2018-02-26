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

    public override void Initialize(ItemData data, Vector2Int rootPos, Inventory placedInventory = null)
    {
        base.Initialize(data, rootPos, placedInventory);
    }

    protected override void Start()
    {
        base.Start();
        if (!bagInventory)
        {
            bagInventory = iManager.NewInventory(Vector3.zero, data.bagInventoryData.width, data.bagInventoryData.height, "Bag Inventory", data.bagInventoryType, false);
            bagInventory.gameObject.SetActive(false);
        }
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
