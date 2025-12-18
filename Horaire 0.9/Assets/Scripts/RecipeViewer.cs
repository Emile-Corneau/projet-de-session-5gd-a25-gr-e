using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeViewer : MonoBehaviour
{
    [SerializeField] private Image[] ingredientSlots = new Image[3];
    [SerializeField] private Image resultSlot;
    [SerializeField] private TMP_Text text;

    [SerializeField] private Recipe[] recipes;

    private int currentIndex = 0;

    private void Start()
    {
        ShowRecipe(currentIndex);
    }

    public void NextRecipe()
    {
        currentIndex++;
        if (currentIndex >= recipes.Length)
            currentIndex = 0;

        ShowRecipe(currentIndex);
    }

    public void PreviousRecipe()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = recipes.Length - 1;

        ShowRecipe(currentIndex);
    }

    public void CloseRecipes() 
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameObject.SetActive(false);
    }

    private void ShowRecipe(int index)
    {
        Recipe recipe = recipes[index];

        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            ingredientSlots[i].sprite = recipe.ingredientSprites[i];
        }

        resultSlot.sprite = recipe.result;
        text.text = recipe.recipeName;
    }
}
