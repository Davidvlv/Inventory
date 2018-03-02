using UnityEngine;
[CreateAssetMenuAttribute(fileName = "InventoryType", menuName = "Inventory/Inventory Type", order = 1)]
public class InventoryType : ScriptableObject {

    public RuleTile ruleTile;
    public float paddingLeft, paddingRight, paddingTop, paddingBottom;
    public float edgeRadius;

    public Sprite closeButton;
    public Sprite closeButtonDown;
    public Sprite minimiseButton;
    public Sprite minimiseButtonDown;
    public Sprite maximiseButton;
    public Sprite maximiseButtonDown;
    public Sprite hotkeyButton;
    public Sprite hotkeyButtonDown;
    public float buttonsPaddingTop, closeButtonPaddingRight, minimiseButtonPaddingRight;
}
