using System;
using TMPro;
using UnityEngine;

public class QuestCardNPC : QuestCard
{

    [SerializeField] private TextMeshProUGUI questRewardTMP;

    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        questRewardTMP.text = $"-{quest.GoldReWard} Gold\n" +
                              $"-{quest.ExpReward} Exp\n" +
                              $"-x{quest.ItemReward.Quantity}{quest.ItemReward.Items.ItemsName}";
    }    

    public void AccpetQuest()
    {
        if (QuestToComplete == null) return;
        QuestToComplete.QuestAccepted = true;
        QuestManager.instance.AcceptQuest(QuestToComplete);
        gameObject.SetActive(false);
    }
}