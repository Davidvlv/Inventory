using UnityEngine;

[CreateAssetMenuAttribute(fileName = "NewSpriteItem", menuName = "Inventory/Item - Sprite", order = 0)]
public class ItemDataSprite : ItemDataBase {
    public Sprite sprite;

    public override void InitializeItem(Item item)
    {
        base.InitializeItem(item);
        
        SpriteRenderer spriteRenderer = item.gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
        spriteRenderer.sprite = sprite;
    }
}
