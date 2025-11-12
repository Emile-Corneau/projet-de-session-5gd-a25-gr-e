using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPickup : MonoBehaviour
{
    [SerializeField] private GameObject crosshair;

    private Outline outline;
    private bool isPickable = false;
    private InventoryManager inventoryManager;

    private void Start()
    {
        if (crosshair == null)
        {
            Debug.LogError("Crosshair GameObject not assigned");
            return;
        }

        outline = crosshair.GetComponent<Outline>();
        if (outline == null)
        {
            Debug.LogError("Outline component missing");
        }

        inventoryManager = InventoryManager.Instance;
    }

    void Update()
    {
        if (outline != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("Pickable"))
                {
                    isPickable = true;
                    outline.enabled = true;
                }
                else
                {
                    isPickable = false;
                    outline.enabled = false;
                }
            }
            else
            {
                isPickable = false;
                outline.enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPickable)
            {
                if (inventoryManager.CheckInvSpace())
                {
                    Debug.Log("There’s space!");
                }
            }
        }
    }
}
