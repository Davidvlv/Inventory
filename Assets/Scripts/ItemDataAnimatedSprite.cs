//using UnityEngine;

//[CreateAssetMenuAttribute(fileName = "NewAnimatedItem", menuName = "Inventory/Item - Animated Sprite", order = 0)]
//public class ItemDataAnimatedSprite : ItemData {
    
//    [SerializeField]
//    public RuntimeAnimatorController controller;

//    public override Item CreateItem(GameObject itemObject)
//    {
//        Item item = base.CreateItem(itemObject);

//        SpriteRenderer spriteRenderer = item.gameObject.AddComponent<SpriteRenderer>();
//        spriteRenderer.sortingOrder = 1;

//        Animator animator = item.gameObject.AddComponent<Animator>();
//        animator.runtimeAnimatorController = controller;

//        return item;
//    }
//}
