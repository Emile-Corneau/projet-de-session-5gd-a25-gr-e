using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public InventoryItem[] Inventory { get; private set; } = new InventoryItem[4];

    [SerializeField] private GameObject[] slotBorders;
    [SerializeField] private GameObject[] slotSprites;

    int activeSlot = 1;
    InventoryItem selectedItem = null;

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
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
            activeSlot++;
        else if (scroll < 0f)
            activeSlot--;

        if (activeSlot > slotBorders.Length) activeSlot = 1;
        if (activeSlot < 1) activeSlot = slotBorders.Length;

        if (Input.GetKeyDown(KeyCode.Alpha1)) activeSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) activeSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) activeSlot = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) activeSlot = 4;

        UpdateBorders();

        selectedItem = Inventory[activeSlot - 1];

    }

    private void UpdateBorders()
    {
        for (int i = 0; i < slotBorders.Length; i++)
        {
            slotBorders[i].SetActive(i == activeSlot - 1);
        }
    }

    public void AddItem(ItemData itemData, int amount = 1)
    {
        if (CheckInvSpace())
        {
            InventoryItem itemToAdd = new InventoryItem(itemData, amount);

            if (selectedItem == null)
            {
                Inventory[activeSlot - 1] = itemToAdd;
                UpdateSlotSprite(activeSlot - 1, itemData.itemIcon);
            }

            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] == null)
                {
                    Inventory[i] = itemToAdd;
                    UpdateSlotSprite(i, itemData.itemIcon);
                    break;
                }
            }
        }
    }

    private void UpdateSlotSprite(int slotIndex, Sprite newIcon)
    {
        Image img = slotSprites[slotIndex].GetComponent<Image>();

        if (newIcon != null)
        {
            img.sprite = newIcon;
            img.enabled = true;
        }
        else
        {
            img.sprite = null;
            img.enabled = false;
        }
    }

    public void RemoveItem(ItemData itemData, int amount = 1)
    {
        selectedItem = null;
    }

    public bool CheckInvSpace()
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == null)
            {
                return true;
            }
        }
        return false;
    }
}