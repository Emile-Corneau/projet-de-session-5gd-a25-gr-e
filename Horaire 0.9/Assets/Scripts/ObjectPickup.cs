using UnityEngine;
using UnityEngine.UI;

public class ObjectPickup : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    [SerializeField] private float pickupDistance = 4f;

    private Outline outline;
    private InventoryManager inventoryManager;

    private ItemPickupData currentPickup;
    private GameObject currentObject;

    private void Start()
    {
        outline = crosshair.GetComponent<Outline>();
        if (outline == null)
            Debug.LogError("Outline component missing on crosshair.");
        
        //Get IM instance
        inventoryManager = InventoryManager.Instance;
    }

    private void Update()
    {
        HandleRaycast();
        HandlePickupInput();
    }

    //Method to check if rayhit is a pickable object and update current variables in accordance
    private void HandleRaycast()
    {
        outline.enabled = false;
        currentPickup = null;
        currentObject = null;

        RaycastHit hit;

        //If not object is returned within pickup distance, skip ahead
        if (!Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupDistance))
        {
            return;
        }

        //If item is not pickable, do not update current variables
        if (!hit.collider.CompareTag("Pickable"))
        {
            return;
        }

        outline.enabled = true;

        currentPickup = hit.collider.GetComponentInParent<ItemPickupData>();
        currentObject = hit.collider.gameObject;
    }

    //Method to check for pickup input
    private void HandlePickupInput()
    {
        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }
        if (currentPickup == null)
        {
            return;
        }
        if (!inventoryManager.CheckInvSpace())
        {
            Debug.Log("Inventory full!");
            return;
        }

        inventoryManager.AddItem(currentPickup.itemData, currentPickup.amount);
        Destroy(currentObject);
    }
}