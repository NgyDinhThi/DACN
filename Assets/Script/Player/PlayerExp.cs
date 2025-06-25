using UnityEngine;
using System;

// Quản lý điểm kinh nghiệm và xử lý lên cấp cho người chơi
public class PlayerExp : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddExp(200f);
        }
    }

    // Thêm EXP và kiểm tra lên cấp
    public void AddExp(float amount)
    {
        stats.CurrentExp += amount;
        stats.TotalExp += amount;

        while (stats.CurrentExp >= stats.NextLevelUp)
        {
            stats.CurrentExp -= stats.NextLevelUp;
            NewLevelGrow();
        }
    }

    // Thực hiện tăng cấp và tính lại ngưỡng EXP
    private void NewLevelGrow()
    {
        stats.level++;
        stats.AttributePoint++;

        float currentExpRequired = stats.NextLevelUp;
        float newNextLevelUp = MathF.Round(currentExpRequired + stats.NextLevelUp * (stats.ExpMultiplier / 100f));

        stats.NextLevelUp = newNextLevelUp;
    }
}
