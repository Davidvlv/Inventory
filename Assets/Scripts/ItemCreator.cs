using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreator : MonoBehaviour {

    private List<ItemData> items = new List<ItemData>();

    public Dropdown itemDropdown;

    public InventoryType inventoryType;

    private InventoryManager iManager;
    private Inventory createInventory;

    void Start () {
        iManager = InventoryManager.instance;

        // Add all ItemData in the Data folder
        Object[] objs = Resources.LoadAll("Data/Items");
        
        foreach(Object obj in objs)
        {
            items.Add((ItemData)obj);
        }

        // sort by name
        items.Sort(ItemData.SortByName);

        // Add options to the dropdown
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        foreach(ItemData item in items)
        {
            options.Add(new Dropdown.OptionData(item.name));
        }
        options.Add(new Dropdown.OptionData("Everything!"));

        itemDropdown.AddOptions(options);

	}

    public void SelectItem(int i)
    {
        if (i <= 0)
        {
            return;
        }
        Inventory newInventory;
        // everything!
        if (i > items.Count)
        {
            newInventory = iManager.NewInventoryWithItems(items, null, inventoryType);
        }
        else
        {
            newInventory = iManager.NewInventoryWithItems(new List<ItemData>() { items[i - 1] }, null, inventoryType);
        }
        if (createInventory != null)
        {
            newInventory.transform.position = createInventory.GetTransposedPosition(newInventory);
            iManager.DestroyInventory(createInventory);
        }
        createInventory = newInventory;
    }
	

}
