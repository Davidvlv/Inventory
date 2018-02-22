using UnityEngine;

[CreateAssetMenuAttribute(fileName = "NewAnimatedItem", menuName = "Inventory/Item - Animated Sprite", order = 0)]
public class ItemDataAnimatedSprite : ItemDataBase {
    
    [SerializeField]
    public RuntimeAnimatorController controller;

    public override void InitializeItem(Item item)
    {
        base.InitializeItem(item);

        SpriteRenderer spriteRenderer = item.gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;

        Animator animator = item.gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = controller;
    }
}
