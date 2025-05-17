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

    [Header("Quest completed")]
    [SerializeField] private GameObject claimButton;
    [SerializeField] private GameObject rewardsPanel;

    private void Update()
    {
       
        statusTMP.text = $"Status\n {QuestToComplete.CurrentStatus}/{QuestToComplete.QuestGoal}";
    }

    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        statusTMP.text = $"Status\n {quest.CurrentStatus}/{quest.QuestGoal}";
        goldRewardTMP.text = quest.GoldReWard.ToString();
        expRewardTMP.text = quest.ExpReward.ToString();
        
        
        itemIcon.sprite = quest.ItemReward.Items.Icon;
        itemQuantityTMP.text = quest.ItemReward.Quantity.ToString();
    }

    public void ClaimQuest()
    {
        GameManager.instance.AddPlayerExp(QuestToComplete.ExpReward);
        Inventory.instance.AddItems(QuestToComplete.ItemReward.Items, QuestToComplete.ItemReward.Quantity);
        CoinsManager.instance.AddCoin(QuestToComplete.GoldReWard);
       gameObject.SetActive(false);
    }    

    private void QuestCompletedCheck()
    {
        if (QuestToComplete.QuestCompleted)
        {
            claimButton.SetActive(true);
            rewardsPanel.SetActive(true);
        }
    }
    private void OnEnable()
    {
        QuestCompletedCheck();
    }
}