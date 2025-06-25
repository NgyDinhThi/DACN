using System;
using UnityEngine;
using UnityEngine.UI;

public class RecipyCard : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image recipyIcon; // Biểu tượng của công thức nấu ăn

    public Recipe RecipeLoad { get; set; } // Công thức nấu ăn hiện tại được tải vào card

    // Khởi tạo RecipyCard với công thức nấu ăn, hiển thị biểu tượng tương ứng
    public void InitRecipyCard(Recipe recipe)
    {
        RecipeLoad = recipe;
        recipyIcon.sprite = recipe.FinalItem.Icon; // Cập nhật biểu tượng công thức
    }

    // Xử lý khi người dùng click vào công thức, hiển thị công thức trong CraftingManager
    public void ClickRecipe()
    {
        CraftingManager.instance.ShowRecipe(RecipeLoad); // Hiển thị công thức trong CraftingManager
    }
}
