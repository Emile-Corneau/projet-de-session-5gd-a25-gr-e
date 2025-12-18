using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public Sprite[] ingredientSprites = new Sprite[3];
    public Sprite result;
    public ItemData[] ingredients = new ItemData[3];
}
