using System;
using TMPro;
using UnityEngine;

// Hiển thị và xử lý tương tác nhiệm vụ từ NPC
public class QuestCardNPC : QuestCard
{
    [SerializeField] private TextMeshProUGUI questRewardTMP;

    // Cấu hình giao diện nhiệm vụ với thông tin phần thưởng
    public override void ConfigQuestUI(Quest quest)
    {
        if (quest.QuestAccepted)
        {
            gameObject.SetActive(false);
            return;
        }

        base.ConfigQuestUI(quest);
        questRewardTMP.text = $"-{quest.GoldReWard} Gold\n" +
                              $"-{quest.ExpReward} Exp\n" +
                              $"-x{quest.ItemReward.Quantity}{quest.ItemReward.Items.ItemsName}";
    }

    // Chấp nhận nhiệm vụ và vô hiệu hóa card
    public void AccpetQuest()
    {
        if (QuestToComplete == null) return;
        QuestToComplete.QuestAccepted = true;
        QuestManager.instance.AcceptQuest(QuestToComplete);
        gameObject.SetActive(false);
    }
}