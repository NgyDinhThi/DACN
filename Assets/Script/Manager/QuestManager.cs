using System;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{

    [Header("Config")]
    [SerializeField] private Quest[] quests;
    [Header("NPC quest panel")]
    [SerializeField] private QuestCardNPC questCardNPCPrefab;
    [SerializeField] protected Transform npcPanelContainer;

    [Header("PLayer quest panel")]
    [SerializeField] private QuestCardPlayer questCardPlayerPrefab;
    [SerializeField] private Transform playerQuestContainer;

    private void Start()
    {
        LoadQuestToNPCPanel();
    }

    public void AcceptQuest(Quest quest)
    {
       QuestCardPlayer cardPlayer = Instantiate(questCardPlayerPrefab, playerQuestContainer);
        cardPlayer.ConfigQuestUI(quest);

    }    

    private void LoadQuestToNPCPanel()
    {
        for (int i = 0; i < quests.Length; i++)
        {
           QuestCard npcCard = Instantiate(questCardNPCPrefab, npcPanelContainer);
           npcCard.ConfigQuestUI(quests[i]);     
        
        }

    }

    private Quest QuestExits(string questId)
    {
        foreach (Quest quests in quests)
        {
            if (quests.ID == questId)
                return quests;
        }
      return null;

    }    

    public void AddProgress(string questID, int amount)
    {
        Quest questToUpdate = QuestExits(questID);
        if (questToUpdate == null) return;
        if (questToUpdate.QuestAccepted)
        {
            questToUpdate.AddProgress(amount);
        }
    }    


    private void OnEnable()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i].ResetQuest();
        }
    }






}