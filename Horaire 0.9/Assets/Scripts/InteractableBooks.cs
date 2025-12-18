using UnityEngine;

public class InteractableBooks : InteractableObject
{
    [SerializeField] GameObject RecipeUI;

    public override void Activate(InventoryItem selectedItem)
    {

        RecipeUI.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
