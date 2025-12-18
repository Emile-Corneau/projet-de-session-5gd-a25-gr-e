using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public InventoryItem[] Inventory { get; private set; } = new InventoryItem[4];

    [SerializeField] private GameObject[] slotBorders;
    [SerializeField] private GameObject[] slotSprites;

    private int activeSlot = 1;
    public InventoryItem selectedItem = null;

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

    private void Update()
    {
        HandleScrollInput();
        HandleNumberKeyInput();

        activeSlot = Mathf.Clamp(activeSlot, 1, slotBorders.Length);

        selectedItem = Inventory[activeSlot - 1];

        UpdateBorders();
    }

    //ScrollWheel input for inventory
    private void HandleScrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
            activeSlot++;
        else if (scroll < 0f)
            activeSlot--;

        //Wrap-around behavior
        if (activeSlot > slotBorders.Length) activeSlot = 1;
        if (activeSlot < 1) activeSlot = slotBorders.Length;
    }

    //ANum input for inventory
    private void HandleNumberKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) activeSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) activeSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) activeSlot = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) activeSlot = 4;
    }

    //Method to enable current active border
    private void UpdateBorders()
    {
        for (int i = 0; i < slotBorders.Length; i++)
        {
            if (slotBorders[i] != null)
                slotBorders[i].SetActive(i == activeSlot - 1);
        }
    }

    //Method to add item to inventory
    public void AddItem(ItemData itemData, int amount = 1)
    {
        if (itemData == null)
        {
            Debug.LogError("Tried to add null itemData");
            return;
        }

        InventoryItem itemToAdd = new InventoryItem(itemData, amount);
        int index = activeSlot - 1;

        //Try to add item to current selected slot
        if (Inventory[index] == null)
        {
            Inventory[index] = itemToAdd;
            UpdateSlotSprite(index, itemData.itemIcon);
            return;
        }

        //Otherwise add to next free slot
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == null)
            {
                Inventory[i] = itemToAdd;
                UpdateSlotSprite(i, itemData.itemIcon);
                return;
            }
        }
    }

    //Method to update sprites when an object is picked up or dropped
    private void UpdateSlotSprite(int slotIndex, Texture newIcon)
    {
        if (slotSprites == null || slotIndex < 0 || slotIndex >= slotSprites.Length)
        {
            return;
        }

        RawImage img = slotSprites[slotIndex]?.GetComponent<RawImage>();

        if (img == null)
        {
            return;
        }

        //Disables rawImage components to avoid white boxes
        if (newIcon != null)
        {
            img.texture = newIcon;
            img.enabled = true;
        }
        else
        {
            img.texture = null;
            img.enabled = false;
        }
    }

    //Remove item from inventory
    public void RemoveItem(ItemData itemData, int amount = 1)
    {
        //Finds item index and clears sprite
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] != null && Inventory[i].itemData == itemData)
            {
                Inventory[i] = null;
                UpdateSlotSprite(i, null);
                break;
            }
        }

        selectedItem = null;
    }

    //Checks if inventory has free space
    public bool CheckInvSpace()
    {
        foreach (var item in Inventory)
        {
            if (item == null)
                return true;
        }
        return false;
    }
}