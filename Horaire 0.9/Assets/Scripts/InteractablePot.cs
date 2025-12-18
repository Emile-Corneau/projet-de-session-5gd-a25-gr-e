using UnityEngine;

public class InteractablePot : InteractableObject
{
    [SerializeField] public GameObject player;

    private InventoryItem[] addedItems = new InventoryItem[3];
    private int count;

    [SerializeField] private Recipe[] allRecipes;

    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = InventoryManager.Instance;
    }

    public override void Activate(InventoryItem selectedItem)
    {
        Debug.Log("Pot interacted with");

        if (selectedItem != null && selectedItem.itemData.item_type.ToString() == "Ingredient")
        {
            for (int i = 0; i < addedItems.Length; i++)
            {
                if (addedItems[i] == null)
                {
                    addedItems[i] = selectedItem;

                    count++;
                    if (count >= addedItems.Length)
                        Brew();

                    inventoryManager.RemoveItem(selectedItem.itemData);

                    return;
                }
            }
        }
    }

    private ItemData[] GetAddedIngredientData()
    {
        ItemData[] result = new ItemData[3];
        for (int i = 0; i < 3; i++)
            result[i] = addedItems[i].itemData;

        return result;
    }

    private Recipe FindMatchingRecipe()
    {
        ItemData[] added = GetAddedIngredientData();
        foreach (ItemData item in added)
        {
            Debug.Log(item.name);
        }

        System.Array.Sort(added, (a, b) => a.name.CompareTo(b.name));
        foreach (ItemData item in added)
        {
            Debug.Log(item.name);
        }

        foreach (Recipe recipe in allRecipes)
        {
            ItemData[] rec = (ItemData[])recipe.ingredients.Clone();
            foreach (ItemData item in rec)
            {
                Debug.Log(item.name);
            }
            System.Array.Sort(rec, (a, b) => a.name.CompareTo(b.name));
            foreach (ItemData item in rec)
            {
                Debug.Log(item.name);
            }

            bool match = true;
            for (int i = 0; i < 3; i++)
            {
                if (added[i] != rec[i])
                {
                    match = false;
                    break;
                }
            }

            if (match)
                return recipe;
        }

        return null;
    }

    public void Brew()
    {
        Debug.Log("brewing");

        Recipe matched = FindMatchingRecipe();

        if (matched != null)
        {
            Debug.Log("Recipe found: " + matched.recipeName);
            // TODO: give potion reward
        }
        else
        {
            Debug.Log("No matching recipe!");
        }

        // Clear pot
        addedItems = new InventoryItem[3];
        count = 0;
    }
}