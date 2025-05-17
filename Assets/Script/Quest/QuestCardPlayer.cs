using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCardPlayer : QuestCard
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI statusTMP;
    [SerializeField] private TextMeshProUGUI goldRewardTMP;
    [SerializeField] private TextMeshProUGUI expRewardTMP;

    [Header("Item")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;

    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        statusTMP.text = $"Status\n {quest.CurrentStatus}/{quest.QuestGoal}";
        goldRewardTMP.text = quest.GoldReWard.ToString();
        expRewardTMP.text = quest.ExpReward.ToString();
        
        
        itemIcon.sprite = quest.ItemReward.Items.Icon;
        itemQuantityTMP.text = quest.ItemReward.Quantity.ToString();
    }


}