using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<InventoryItem> inventory = new List<InventoryItem>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddItem(ItemData itemData, int amount = 1)
    {
        if (inventory.Count < 4) {
            InventoryItem itemToAdd = new InventoryItem(itemData, itemData.maxStackSize);
            inventory.Add(itemToAdd);
        }
        return true;
    }

    public void RemoveItem(ItemData itemToRemove, int amount = 1)
    {
        // Logic to remove item, handle stack reduction, etc.
    }
}