using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public string Name;
    public Sprite[] ingredients = new Sprite[3];
    public Sprite result;
}