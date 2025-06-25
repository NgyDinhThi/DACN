using System;
using UnityEngine;

// Quản lý nhiệm vụ, khởi tạo và cập nhật tiến độ nhiệm vụ
public class QuestManager : Singleton<QuestManager>
{
    [Header("Config")]
    [SerializeField] private Quest[] quests; // Danh sách nhiệm vụ
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
    private void LoadQuestToNPCPanel()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            QuestCard npcCard = Instantiate(questCardNPCPrefab, npcPanelContainer);
            npcCard.ConfigQuestUI(quests[i]);
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
}