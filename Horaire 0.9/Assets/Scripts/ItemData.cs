using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("properties")]
    public Sprite itemIcon;
    public itemType item_type;
    public int maxStackSize = 1;
}

public enum itemType {Potion, Ingredient};