using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    private bool invHasSpace = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (invHasSpace)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange))
                {
                    if (hit.collider.CompareTag("Pickable"))
                    {

                    }
                }
            }
            else
            {

            }
        }
    }
}