using System;
using UnityEngine;

// Lưu trữ thông tin nhiệm vụ và quản lý tiến độ
[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    [Header("Info")]
    public string Name;
    public string ID;
    public int QuestGoal;

    [Header("Description")]
    [TextArea] public string Description;

    [Header("Reward")]
    public int GoldReWard;
    public float ExpReward;
    public QuestItemReward ItemReward;

    [HideInInspector] public int CurrentStatus;
    [HideInInspector] public bool QuestCompleted;
    [HideInInspector] public bool QuestAccepted;

    // Đánh dấu nhiệm vụ hoàn thành nếu chưa hoàn thành
    private void QuestIsCompleted()
    {
        if (QuestCompleted) return;
        QuestCompleted = true;
    }

    // Đặt lại trạng thái nhiệm vụ
    public void ResetQuest()
    {
        QuestAccepted = false;
        QuestCompleted = false;
        CurrentStatus = 0;
    }

    // Cập nhật tiến độ nhiệm vụ
    public void AddProgress(int amount)
    {
        CurrentStatus += amount;
        if (CurrentStatus >= QuestGoal)
        {
            CurrentStatus = QuestGoal;
            QuestIsCompleted();
        }
    }
}

// Lưu trữ thông tin vật phẩm thưởng của nhiệm vụ
[Serializable]
public class QuestItemReward
{
    public InventoryItems Items;
    public int Quantity;
}