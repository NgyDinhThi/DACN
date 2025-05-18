using UnityEngine;
using System;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecipyCard recipyCardPrefab;
    [SerializeField] private Transform RecipyContainer;
    [Header("Recipy")]
    [SerializeField] private RecipeList recipies;


    private void Start()
    {
        LoadRecipy();
    }

    private void LoadRecipy()
    {
        for (int i = 0; i < recipies.Recipes.Length; i++)
        {
            RecipyCard card = Instantiate(recipyCardPrefab, RecipyContainer);
            card.InitRecipyCard(recipies.Recipes[i]);
        
        }


    }  
        

}