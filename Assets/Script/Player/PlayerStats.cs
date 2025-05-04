using UnityEngine;
using System;

// Cho phép tạo ScriptableObject từ menu Unity
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")]
    public int level; // Cấp độ hiện tại của nhân vật

    [Header("Health info")] // Thông tin liên quan đến máu
    public float health; // Máu hiện tại
    public float Max_health; // Máu tối đa ban đầu

    [Header("Mana info")] // Thông tin liên quan đến mana
    public float mana; // Lượng mana hiện tại
    public float Max_mana; // Mana tối đa ban đầu

    [Header("Exp info")] // Thông tin kinh nghiệm (EXP)
    public float CurrentExp; // EXP hiện tại của nhân vật
    public float NextLevelUp; // Lượng EXP cần để lên cấp tiếp theo
    public float InitialNextLevelExp; // EXP cần để lên cấp từ level 1 → 2
    [Range(1f, 100f)] public float ExpMultiplier; // Hệ số nhân EXP để tăng cấp độ nhanh/chậm

    [Header("Attack")] // Thông tin tấn công
    public float BaseDmg; // Sát thương cơ bản
    public float CritChance; // Tỉ lệ chí mạng (0–100%)
    public float CritDmg; // Hệ số sát thương khi chí mạng (VD: 2.0x)

    // Hàm dùng để reset lại nhân vật về trạng thái ban đầu khi bắt đầu lại game hoặc revive
    public void ResetPlayer()
    {
        health = Max_health; // Hồi đầy máu
        mana = Max_mana; // Hồi đầy mana
        level = 1; // Trở về cấp độ 1
        CurrentExp = 0f; // Xóa kinh nghiệm hiện tại
        NextLevelUp = InitialNextLevelExp; // Thiết lập lại ngưỡng EXP ban đầu để lên cấp
    }
}
