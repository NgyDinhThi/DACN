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

    // mission info
    public List<string> acceptedQuestIDs;      
    public List<int> questProgressValues;       
    public List<bool> questCompletions;         

    // inventory info
    public List<string> itemIds;
    public List<int> quantities;


    // coin info
    public float Coins;

    // weapon info
    public string equippedWeaponName;

    public DataStore(PlayerStats stats, Transform playerTransform, QuestManager questManager, Inventory inventory)
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

        // mission
        acceptedQuestIDs = new List<string>();
        questProgressValues = new List<int>();
        questCompletions = new List<bool>();

        // coin
        Coins = CoinsManager.instance.Coins;



        foreach (Quest quest in questManager.Quests)
        {
            if (quest.QuestAccepted)
            {
                acceptedQuestIDs.Add(quest.ID);
                questProgressValues.Add(quest.CurrentStatus);
                questCompletions.Add(quest.QuestCompleted);
            }
        }

        // inventory
        itemIds = new List<string>();
        quantities = new List<int>();

        if (inventory != null && inventory.inventoryItems != null)
        {
            foreach (InventoryItems item in inventory.inventoryItems)
            {
                if (item != null) 
                {
                    itemIds.Add(item.Id);
                    quantities.Add(item.quantity);
                }
            }
        }

    }
}