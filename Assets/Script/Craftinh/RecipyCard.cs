using System;
using UnityEngine;
using UnityEngine.UI;

public class RecipyCard : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image recipyIcon;
    
    public Recipe RecipeLoad {  get; set; }
    
    public void InitRecipyCard(Recipe recipe)
    {
        RecipeLoad = recipe;
        recipyIcon.sprite = recipe.FinalItem.Icon;

    }    


}