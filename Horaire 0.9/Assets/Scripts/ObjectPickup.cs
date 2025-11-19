using UnityEngine;
using UnityEngine.UI;

public class ObjectPickup : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;

    private Outline outline;
    private bool isPickable = false;
    private InventoryManager inventoryManager;

    private GameObject obj;
    public ItemPickupData pickup;

    private void Start()
    {
        outline = crosshair.GetComponent<Outline>();
        if (outline == null)
        {
            Debug.LogError("Outline component missing");
        }

        inventoryManager = InventoryManager.Instance;
    }

    void Update()
    {
        RaycastHit hit;

        if (outline != null)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
            {
                if (hit.collider.CompareTag("Pickable"))
                {
                    outline.enabled = true;
                    isPickable = true;

                    pickup = hit.collider.GetComponentInParent<ItemPickupData>();
                    obj = hit.collider.gameObject;
                }
                else
                {
                    outline.enabled = false;
                    isPickable = false;
                    pickup = null;
                    obj = null;
                }
            }
            else
            {
                outline.enabled = false;
                isPickable = false;
                pickup = null;
                obj = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && isPickable)
        {
            if (pickup != null)
            {
                if (inventoryManager.CheckInvSpace())
                {
                    inventoryManager.AddItem(pickup.itemData, pickup.amount);

                    Destroy(obj);
                    Debug.Log("Picked up " + pickup.itemData.item_type);
                }
                else
                {
                    Debug.Log("Inventory full!");
                }
            }
        }
    }
}