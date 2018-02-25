using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreator : MonoBehaviour {

    private List<ItemData> items = new List<ItemData>();

    public GameObject itemPrefab;
    public Inventory createInventory;

    public Dropdown itemDropdown;

    public InventoryType greenInventory;

    private InventoryManager iManager;

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
        // everything!
        if (i > items.Count)
        {
            iManager.NewInventoryWithItems(items, "New Items", greenInventory);
            return;
        }
        // option[0] is "Choose an item"
        iManager.NewInventoryWithItems(new List<ItemData>() { items[i - 1] }, "New Items", greenInventory);
    }
	

}
