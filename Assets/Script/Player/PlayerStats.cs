using UnityEngine;
using System;

// Enum các chỉ số thuộc tính của nhân vật
public enum Attribute
{
    Strength,
    Dexterity,
    Intelligence
}

// Lưu trữ và quản lý toàn bộ chỉ số của nhân vật
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")]
    public int level;

    [Header("Health info")]
    public float health;
    public float Max_health;

    [Header("Mana info")]
    public float mana;
    public float Max_mana;

    [Header("Exp info")]
    public float CurrentExp;
    public float NextLevelUp;
    public float InitialNextLevelExp;
    [Range(1f, 100f)] public float ExpMultiplier;

    [Header("Attack")]
    public float BaseDmg;
    public float CritChance;
    public float CritDmg;

    [Header("Attribute")]
    public int Strength;
    public int Dexterity;
    public int Intelligence;
    public int AttributePoint;

    [HideInInspector] public float TotalExp;
    [HideInInspector] public float TotalDmg;

    // Đặt lại toàn bộ chỉ số nhân vật về trạng thái khởi đầu
    public void ResetPlayer()
    {
        health = Max_health;
        mana = Max_mana;
        level = 1;
        CurrentExp = 0f;
        NextLevelUp = InitialNextLevelExp;
        TotalExp = 0f;
        BaseDmg = 3f;
        CritChance = 10;
        CritDmg = 50;
        Strength = 0;
        Dexterity = 0;
        Intelligence = 0;
        AttributePoint = 0;
    }
}
