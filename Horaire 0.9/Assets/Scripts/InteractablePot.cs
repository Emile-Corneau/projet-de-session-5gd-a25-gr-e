using UnityEngine;

public class InteractablePot : InteractableObject
{
    public override void Activate()
    {
        Debug.Log("Pot interacted with");
    }
}
