using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


[CreateAssetMenuAttribute(fileName = "NewAnimatedItem", menuName = "Inventory/Item - Animated Sprite", order = 0)]
public class ItemDataAnimatedSprite : ItemDataBase {

    public AnimatorController controller;

    public override void InitializeItem(Item item)
    {
        base.InitializeItem(item);

        SpriteRenderer spriteRenderer = item.gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;

        Animator animator = item.gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = controller as RuntimeAnimatorController;
    }
}
