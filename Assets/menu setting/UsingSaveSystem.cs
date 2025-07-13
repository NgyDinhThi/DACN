using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class UsingSaveSystem : MonoBehaviour
{
    public string sceneToLoad = "DACN";

    [Header("Config")]
    [SerializeField] private GameObject menu;
    [SerializeField] private Player player;

    public static bool isPause;
    private void Start()
    {
        menu.SetActive(false);
        isPause = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
           ClosePanel();
        }
    }
    private void ClosePanel()
    {
        menu?.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }    

    public void UseSave()
    {
        SaveSystem.Saveplayer(player);
        Debug.Log("Save thành công! File được lưu tại: " + SaveSystem.path);
    }

    public void UseLoadPlayer()
    {
        DataStore data = SaveSystem.LoadPlayer();

        // Gán dữ liệu vào PlayerStats
        PlayerStats stats = player.Stats;
        stats.level = data.level;
        stats.health = data.health;
        stats.Max_health = data.Max_health;
        stats.mana = data.mana;
        stats.Max_mana = data.Max_mana;
        stats.CurrentExp = data.CurrentExp;
        stats.NextLevelUp = data.NextLevelUp;
        stats.InitialNextLevelExp = data.InitialNextLevelExp;
        stats.BaseDmg = data.BaseDmg;
        stats.CritChance = data.CritChance;
        stats.CritDmg = data.CritDmg;
        stats.Strength = data.Strength;
        stats.Dexterity = data.Dexterity;
        stats.Intelligence = data.Intelligence;
        stats.AttributePoint = data.AttributePoint;
        stats.TotalExp = data.TotalExp;
        stats.TotalDmg = data.TotalDmg;
       

        Vector3 LoadPosition = new Vector3(data.position[0], data.position[1], data.position[2]);
        player.transform.position  = LoadPosition;


        // ----------- LOAD INVENTORY ----------------
        Inventory inventory = Inventory.instance;
        inventory.ResetInventory(); // clear inventory trước
        for (int i = 0; i < data.itemIds.Count; i++)
        {
            string id = data.itemIds[i];
            int quantity = data.quantities[i];

            // tìm item từ GameContents
            InventoryItems found = inventory.IsItemsExistInGamecontents(id);
            if (found != null)
            {
                InventoryItems copy = found.CopyItem();
                copy.quantity = quantity;
                inventory.AddItems(copy, quantity);
            }
        }

        CoinsManager.instance.SetCoins(data.Coins);
        QuestManager questManager = QuestManager.instance;
        questManager.ResetAllQuests();
        questManager.ClearPlayerQuestUI();
        for (int i = 0; i < data.acceptedQuestIDs.Count; i++)
        {
            Quest quest = questManager.GetQuestByID(data.acceptedQuestIDs[i]);
            if (quest != null)
            {
                quest.QuestAccepted = true;
                quest.CurrentStatus = data.questProgressValues[i];
                quest.QuestCompleted = data.questCompletions[i];
                questManager.AddQuestToUI(quest);
            }
        }

    }    
}
