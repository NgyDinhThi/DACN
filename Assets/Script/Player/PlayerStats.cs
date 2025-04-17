using UnityEngine;
using System;

// Cho phép tạo ScriptableObject từ menu Unity
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")]
    public int level; // Cấp độ của nhân vật

    [Header("Health info")] // Thông tin về máu của nhân vật
    public float health; // Máu hiện tại của nhân vật
    public float Max_health; // Máu tối đa của nhân vật

    [Header("Mana info:")]
    public float mana;   // Mana hiện tại của nhân vật
    public float Max_mana;  // Mana tối đa của nhân vật

    [Header("Exp  info:")]
    public float CurrentExp; // EXP hiện tại của nhân vật
    public float NextLevelUp; // Ngưỡng EXP cần đạt để lên cấp
    public float InitialNextLevelExp; // Giá trị EXP ban đầu cần để lên cấp
    [Range(1f, 100f)] public float ExpMultiplier; // Tỷ lệ tăng EXP cần thiết mỗi cấp

    // Hàm reset lại các chỉ số của nhân vật về trạng thái ban đầu
    public void ResetPlayer()
    {
        health = Max_health; // Hồi đầy máu
        mana = Max_mana; // Hồi đầy mana
        level = 1; // Đặt lại cấp độ
        CurrentExp = 0f; // Xóa EXP hiện tại
        NextLevelUp = InitialNextLevelExp; // Reset lại mức EXP cần thiết cho cấp 2
    }
}
