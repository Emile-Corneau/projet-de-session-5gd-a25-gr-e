using UnityEngine;

public class InteractableBooks : InteractableObject
{
    public override void Activate()
    {
        Debug.Log("Books interacted with");
    }
}
