[System.Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int stackSize;

    public InventoryItem(ItemData data, int size)
    {
        itemData = data;
        stackSize = size;
    }
}