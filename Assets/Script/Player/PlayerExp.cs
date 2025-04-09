using UnityEngine;
using System;

public class PlayerExp : MonoBehaviour
{
    [Header("Config")]  
    [SerializeField] private PlayerStats stats;


    private void Update()
    {
        // Kiểm tra nếu phím X được nhấn, nhân vật sẽ nhận 200 EXP
        if (Input.GetKeyDown(KeyCode.X))
        {
           AddExp(200f);
        }
    }
    public void AddExp(float Quantity)
    {
        stats.CurrentExp += Quantity; 
        while (stats.CurrentExp >= stats.NextLevelUp)
        {
            stats.CurrentExp -= stats.NextLevelUp;
            NewLevelGrow();
        }
    }

    private void NewLevelGrow()
    {
        stats.level++;
        float currentExpRequired = stats.NextLevelUp;
        float newNextLevelUp = MathF.Round(  currentExpRequired + stats.NextLevelUp * (stats.ExpMultiplier / 100f));
        stats.NextLevelUp = newNextLevelUp; 
    }
}