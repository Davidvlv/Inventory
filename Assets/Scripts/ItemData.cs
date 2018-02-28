using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    normal, bag
}

public enum RenderType
{
    sprite, animated
}

[CreateAssetMenuAttribute(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class ItemData : ScriptableObject
{
    #region Sorting Functions
    public static int SortByName(ItemData i1, ItemData i2)
    {
        return i1.name.CompareTo(i2.name);
    }

    public static int SortBySize(ItemData i1, ItemData i2)
    {
        return i2.slotPositions.Count.CompareTo(i1.slotPositions.Count);
    }
    #endregion

    public new string name;
    // Bottom Left Corner is (0, 0)
    public List<Vector2Int> slotPositions;

    [Header("Type")]
    public ItemType type;
    // type variables
    // bag
    public InventoryData bagInventoryData;
    public InventoryType bagInventoryType;

    [Header("Rendering")]
    public RenderType render;
    // render variables
    public Sprite sprite;

    [SerializeField]
    public RuntimeAnimatorController controller;

    public virtual Item CreateItem(GameObject itemObject)
    {
        Item item = null;
        switch (type)
        {
            case ItemType.normal: item = itemObject.AddComponent<Item>();         break;
            case ItemType.bag:  item = itemObject.AddComponent<ItemBag>();      break;
        }

        switch (render)
        {
            case RenderType.sprite:
                AddSpriteRenderer(item);
                break;

            case RenderType.animated:
                AddSpriteRenderer(item);
                Animator animator = item.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = controller;
                break;
        }
        return item;
    }

    private void AddSpriteRenderer(Item item)
    {
        SpriteRenderer s_spriteRenderer = item.gameObject.AddComponent<SpriteRenderer>();
        s_spriteRenderer.sortingOrder = 1;
        s_spriteRenderer.sprite = sprite;
    }
}