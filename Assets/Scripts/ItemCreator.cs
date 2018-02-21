using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreator : MonoBehaviour {

    private List<ItemDataBase> items = new List<ItemDataBase>();

    public GameObject itemPrefab;
    public Inventory createInventory;

    public Dropdown itemDropdown;

    void Start () {
        // Add all ItemData in the Data folder
        Object[] objs = Resources.LoadAll("Data");
        
        foreach(Object obj in objs)
        {
            //items.Add((ItemDataBase)obj);
        }

        // sort by name
        items.Sort(ItemDataBase.SortByName);

        // Add options to the dropdown
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        foreach(ItemDataBase item in items)
        {
            //options.Add(new Dropdown.OptionData(item.name, item.sprite));
        }
        itemDropdown.AddOptions(options);

	}

    public void SelectItem(int i)
    {
        // option[0] is "Choose an item"
        CreateItem(items[i - 1]);
    }

    public void CreateItem(ItemDataBase item)
    {
        Instantiate(itemPrefab);
    }
	

}
