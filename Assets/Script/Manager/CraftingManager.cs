using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Button = UnityEngine.UI.Button;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecipyCard recipyCardPrefab;
    [SerializeField] private Transform RecipyContainer;
    [Header("Reccipy info")]
    [SerializeField] private Image item1Icon;
    [SerializeField] private TextMeshProUGUI item1Name;
    [SerializeField] private TextMeshProUGUI item1Amount;
    [SerializeField] private Image item2Icon;
    [SerializeField] private TextMeshProUGUI item2Name;
    [SerializeField] private TextMeshProUGUI item2Amount;
    [SerializeField] private Button craftButton;
    [SerializeField] private TextMeshProUGUI recipyname;
    [Header("Final items")]
    [SerializeField] private Image finalItem;
    [SerializeField] private TextMeshProUGUI finalItemName;
    [SerializeField] private TextMeshProUGUI finalItemDescription;
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

    public void ShowRecipe(Recipe recipe)
    {
        item1Icon.sprite = recipe.Item1.Icon;
        item1Name.text = recipe.Item1.ItemsName;
        item2Icon.sprite = recipe.Item2.Icon;
        item2Name.text = recipe.Item2.ItemsName;

        item1Amount.text = $"{Inventory.instance.GetItemsCurrentStock(recipe.Item1.Id)}/{recipe.Item1Amount}";
        item2Amount.text = $"{Inventory.instance.GetItemsCurrentStock(recipe.Item2.Id)}/{recipe.Item2Amount}";

        finalItem.sprite = recipe.FinalItem.Icon;
        finalItemName.text = recipe.FinalItem.ItemsName;
        finalItemDescription.text = recipe.FinalItem.description;

        craftButton.interactable = CanCrapItem(recipe);

        recipyname.text = recipe.Name;
    }   
    
    private bool CanCrapItem(Recipe recipe)
    {
        int item1Stock = Inventory.instance.GetItemsCurrentStock(recipe.Item1.Id);
        int item2Stock = Inventory.instance.GetItemsCurrentStock(recipe.Item2.Id);
        if (item1Stock >= recipe.Item1Amount && item2Stock >= recipe.Item2Amount)
        {
            return true;
        }
        return false;
    }    

}