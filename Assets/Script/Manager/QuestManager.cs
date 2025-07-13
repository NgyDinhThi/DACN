using System;
using UnityEngine;

// Quản lý nhiệm vụ, khởi tạo và cập nhật tiến độ nhiệm vụ
public class QuestManager : Singleton<QuestManager>
{
    [Header("Config")]
    [SerializeField] private Quest[] quests; // Danh sách nhiệm vụ
    public Quest[] Quests => quests;
    [Header("NPC quest panel")]
    [SerializeField] private QuestCardNPC questCardNPCPrefab; // Mẫu thẻ nhiệm vụ NPC
    [SerializeField] protected Transform npcPanelContainer; // Container chứa thẻ nhiệm vụ NPC

    [Header("PLayer quest panel")]
    [SerializeField] private QuestCardPlayer questCardPlayerPrefab; // Mẫu thẻ nhiệm vụ người chơi
    [SerializeField] private Transform playerQuestContainer; // Container chứa thẻ nhiệm vụ người chơi

    // Khởi tạo nhiệm vụ vào panel NPC
    private void Start()
    {
        LoadQuestToNPCPanel();
    }

    // Thêm nhiệm vụ vào panel người chơi khi chấp nhận
    public void AcceptQuest(Quest quest)
    {
        QuestCardPlayer cardPlayer = Instantiate(questCardPlayerPrefab, playerQuestContainer);
        cardPlayer.ConfigQuestUI(quest);
    }

    // Tạo thẻ nhiệm vụ NPC cho từng nhiệm vụ
    public void LoadQuestToNPCPanel()
    {
        foreach (Quest quest in quests)
        {
            if (!quest.QuestAccepted) // chỉ hiện những quest chưa được nhận
            {
                QuestCard npcCard = Instantiate(questCardNPCPrefab, npcPanelContainer);
                npcCard.ConfigQuestUI(quest);
            }
        }
    }


    // Tìm nhiệm vụ theo ID
    private Quest QuestExits(string questId)
    {
        foreach (Quest quest in quests)
        {
            if (quest.ID == questId)
                return quest;
        }
        return null;
    }

    // Cập nhật tiến độ nhiệm vụ theo ID
    public void AddProgress(string questID, int amount)
    {
        Quest questToUpdate = QuestExits(questID);
        if (questToUpdate == null) return;
        if (questToUpdate.QuestAccepted)
        {
            questToUpdate.AddProgress(amount);
        }
    }

    // Đặt lại tất cả nhiệm vụ khi kích hoạt
    private void OnEnable()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i].ResetQuest();
        }
    }

    public void ResetAllQuests()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i].ResetQuest();
        }
     
    }

    public void ClearPlayerQuestUI()
    {
        foreach (Transform child in playerQuestContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public Quest GetQuestByID(string id)
    {
        return Array.Find(quests, q => q.ID == id);
    }

    public void AddQuestToUI(Quest quest)
    {

        QuestCardPlayer cardPlayer = GameObject.Instantiate(questCardPlayerPrefab, playerQuestContainer);
        cardPlayer.ConfigQuestUI(quest);
    }

    public void ClearNPCQuestUI()
    {
        foreach (Transform child in npcPanelContainer)
        {
            Destroy(child.gameObject);
        }
    }
}