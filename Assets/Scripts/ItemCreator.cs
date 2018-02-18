using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreator : MonoBehaviour {

    private List<ItemData> items = new List<ItemData>();

    public GameObject itemPrefab;
    public Inventory createInventory;

    public Dropdown itemDropdown;

    void Start () {
        // Add all ItemData in the Data folder
        Object[] objs = Resources.LoadAll("Data", typeof(ItemData));
        
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
            options.Add(new Dropdown.OptionData(item.name, item.sprite));
        }
        itemDropdown.AddOptions(options);

	}

    public void SelectItem(int i)
    {
        // option[0] is "Choose an item"
        CreateItem(items[i - 1]);
    }

    public void CreateItem(ItemData item)
    {
        Instantiate(itemPrefab);
    }
	

}
