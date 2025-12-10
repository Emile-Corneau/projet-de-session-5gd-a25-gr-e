using UnityEngine;

public class InteractableBooks : InteractableObject
{
    [SerializeField] GameObject RecipeUI;

    public override void Activate()
    {
        Debug.Log("Books interacted with");

        RecipeUI.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
