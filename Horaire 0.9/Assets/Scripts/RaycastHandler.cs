using UnityEngine;
using UnityEngine.UI;
public class RaycastHandler : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;
    [SerializeField] private float pickupDistance = 4f;

    private Outline outline;
    private InventoryManager inventoryManager;

    private InteractableObject currentInteract;
    private ItemPickupData currentPickup;
    private GameObject currentObject;

    private void Start()
    {
        outline = crosshair.GetComponent<Outline>();
        if (outline == null) Debug.LogError("Outline component missing on crosshair.");

        inventoryManager = InventoryManager.Instance;
    }

    private void Update()
    {
        HandleRaycast();
        HandlePickupInput();
        HandleInteractInput();
    }

    //Method to check the tag of a rayhit and update current variables in accordance
    private void HandleRaycast()
    {
        outline.enabled = false;
        currentPickup = null;
        currentInteract = null;
        currentObject = null;

        RaycastHit hit;

        if (!Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupDistance))
            return;

        // check if rayhit is a pickable
        if (hit.collider.CompareTag("Pickable"))
        {
            outline.enabled = true;
            currentPickup = hit.collider.GetComponentInParent<ItemPickupData>();
            currentObject = hit.collider.gameObject;
            return;
        }

        // check if rayhit is an Interactable
        if (hit.collider.CompareTag("Interactable"))
        {
            outline.enabled = true; 
            currentInteract = hit.collider.GetComponentInParent<InteractableObject>();
            return;
        }
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

    //Method to check for interact input
    private void HandleInteractInput()
    {
        if (!Input.GetKeyDown(KeyCode.F))
        {
            return;
        }

        if (currentInteract == null)
        {
            return;
        }

        currentInteract.Activate();
    }
}