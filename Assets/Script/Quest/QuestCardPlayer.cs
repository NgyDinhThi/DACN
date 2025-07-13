using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Hiển thị và xử lý tương tác nhiệm vụ của người chơi
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

    // Cập nhật trạng thái nhiệm vụ mỗi frame
    private void Update()
    {
        if (QuestToComplete != null)
        {
            statusTMP.text = $"Status\n {QuestToComplete.CurrentStatus}/{QuestToComplete.QuestGoal}";
        }
    }


    // Cấu hình giao diện nhiệm vụ với thông tin chi tiết
    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        statusTMP.text = $"Status\n {quest.CurrentStatus}/{quest.QuestGoal}";
        goldRewardTMP.text = quest.GoldReWard.ToString();
        expRewardTMP.text = quest.ExpReward.ToString();
        itemIcon.sprite = quest.ItemReward.Items.Icon;
        itemQuantityTMP.text = quest.ItemReward.Quantity.ToString();
        QuestCompletedCheck();
    }

    // Nhận phần thưởng và vô hiệu hóa card
    public void ClaimQuest()
    {
        GameManager.instance.AddPlayerExp(QuestToComplete.ExpReward);
        Inventory.instance.AddItems(QuestToComplete.ItemReward.Items, QuestToComplete.ItemReward.Quantity);
        CoinsManager.instance.AddCoin(QuestToComplete.GoldReWard);
        gameObject.SetActive(false);
    }

    // Kiểm tra trạng thái hoàn thành nhiệm vụ
    private void QuestCompletedCheck()
    {
        if (QuestToComplete != null && QuestToComplete.QuestCompleted)
        {
            claimButton.SetActive(true);
            rewardsPanel.SetActive(true);
        }
    }

    // Kích hoạt kiểm tra nhiệm vụ khi card được bật
    private void OnEnable()
    {
        QuestCompletedCheck();
    }
}