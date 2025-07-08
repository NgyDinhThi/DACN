using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class DataStore
{
    // level info
    public int level;

    // health info
    public float health;
    public float Max_health;

    // mana info
    public float mana;
    public float Max_mana;

    // EXP info
    public float CurrentExp;
    public float NextLevelUp;
    public float InitialNextLevelExp;
   

    // attack info
    public float BaseDmg;
    public float CritChance;
    public float CritDmg;

    // attribute info
    public int Strength;
    public int Dexterity;
    public int Intelligence;
    public int AttributePoint;

    // other info
    public float TotalExp;
    public float TotalDmg;

    // position info
    public float[] position;

    public DataStore(PlayerStats stats, Transform playerTransform)
    {
        // Level & stats
        level = stats.level;

        // Health & Mana
        health = stats.health;
        Max_health = stats.Max_health;
        mana = stats.mana;
        Max_mana = stats.Max_mana;

        // EXP
        CurrentExp = stats.CurrentExp;
        NextLevelUp = stats.NextLevelUp;
        InitialNextLevelExp = stats.InitialNextLevelExp;

        // Combat
        BaseDmg = stats.BaseDmg;
        CritChance = stats.CritChance;
        CritDmg = stats.CritDmg;

        // Attributes
        Strength = stats.Strength;
        Dexterity = stats.Dexterity;
        Intelligence = stats.Intelligence;
        AttributePoint = stats.AttributePoint;

        // Other
        TotalExp = stats.TotalExp;
        TotalDmg = stats.TotalDmg;

        // Position
        position = new float[3];
        position[0] = playerTransform.position.x;
        position[1] = playerTransform.position.y;
        position[2] = playerTransform.position.z;
    }
}