using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreator : MonoBehaviour {

    private List<ItemDataBase> items = new List<ItemDataBase>();

    public GameObject itemPrefab;
    public Inventory createInventory;

    public Dropdown itemDropdown;

    private InventoryManager iManager;

    void Start () {
        iManager = InventoryManager.instance;

        // Add all ItemData in the Data folder
        Object[] objs = Resources.LoadAll("Data/Items");
        
        foreach(Object obj in objs)
        {
            items.Add((ItemDataBase)obj);
        }

        // sort by name
        items.Sort(ItemDataBase.SortByName);

        // Add options to the dropdown
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        foreach(ItemDataBase item in items)
        {
            options.Add(new Dropdown.OptionData(item.name));
        }
        options.Add(new Dropdown.OptionData("Everything!"));

        itemDropdown.AddOptions(options);

	}

    public void SelectItem(int i)
    {
        Debug.Log(i);
        // everything!
        if (i > items.Count)
        {
            iManager.NewInventoryWithItems(items);
            return;
        }
        // option[0] is "Choose an item"
        iManager.NewInventoryWithItems(new List<ItemDataBase>() { items[i - 1] });
    }
	

}
