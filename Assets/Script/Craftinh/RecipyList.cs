using System;
using UnityEngine;

// Quản lý danh sách các công thức trong game
[CreateAssetMenu()]
public class RecipeList : ScriptableObject
{
    public Recipe[] Recipes; // Mảng chứa các công thức 
}
