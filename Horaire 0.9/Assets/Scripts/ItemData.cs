using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("properties")]
    public Texture itemIcon;
    public itemType item_type;
    public itemColor item_color;
    public itemSize item_size;
    public int maxStackSize = 1;
}

public enum itemType { Potion, Ingredient };
public enum itemColor { Green, Purple, RedRound, RedSquare };
public enum itemSize { Small, Medium, Large };