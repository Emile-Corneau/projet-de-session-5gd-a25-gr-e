using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public void Activate()
    {
        Debug.Log("Object interacted with!");
        // open door, press button, etc.
    }
}