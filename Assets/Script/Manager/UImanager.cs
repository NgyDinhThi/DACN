using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Quản lý giao diện người chơi và các panel tương tác
public class UImanager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStats stats;

    [Header("Stats bars")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image expBar;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI healthTMP;
    [SerializeField] private TextMeshProUGUI levelTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI coinsTMP;

    [Header("Stats Panel")]
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TextMeshProUGUI statslv;
    [SerializeField] private TextMeshProUGUI statsdmg;
    [SerializeField] private TextMeshProUGUI statscritc;
    [SerializeField] private TextMeshProUGUI statcritdmg;
    [SerializeField] private TextMeshProUGUI statstotalexp;
    [SerializeField] private TextMeshProUGUI statscurrentexp;
    [SerializeField] private TextMeshProUGUI statsreqExp;
    [SerializeField] private TextMeshProUGUI attributepoint;
    [SerializeField] private TextMeshProUGUI strength;
    [SerializeField] private TextMeshProUGUI dexterity;
    [SerializeField] private TextMeshProUGUI intelligence;

    [Header("Extra Pnael")]
    [SerializeField] private GameObject npcQuestPanel;
    [SerializeField] private GameObject playerQuestPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject craftingPanel;

    // Cập nhật thanh máu, mana, kinh nghiệm và văn bản
    private void Update()
    {
        UpdatePlayerUI();
    }

    // Mở/đóng panel chỉ số nhân vật
    public void OpenCloseStsPanel()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
        if (statsPanel.activeSelf)
            UpdateStatsPanel();
    }

    // Mở/đóng panel nhiệm vụ NPC
    public void OpenCloseNPCQuestPanel(bool value)
    {
        npcQuestPanel.SetActive(!npcQuestPanel.activeSelf);
    }

    // Mở/đóng panel nhiệm vụ người chơi
    public void OpenClosePlayerQuestPanel(bool value)
    {
        playerQuestPanel.SetActive(!playerQuestPanel.activeSelf);
    }

    // Mở/đóng panel cửa hàng
    public void OpenCloseShopPanel(bool value)
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    // Mở/đóng panel chế tạo
    public void OpenCloseCraftPanel(bool value)
    {
        craftingPanel.SetActive(!craftingPanel.activeSelf);
    }

    // Cập nhật giao diện thanh máu, mana, kinh nghiệm, cấp độ, tiền
    private void UpdatePlayerUI()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, stats.health / stats.Max_health, Time.deltaTime * 10f);
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, stats.mana / stats.Max_mana, Time.deltaTime * 10f);
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, stats.CurrentExp / stats.NextLevelUp, Time.deltaTime * 10f);
        healthTMP.text = $"{stats.health}/{stats.Max_health}";
        levelTMP.text = $"Level {stats.level}";
        manaTMP.text = $"{stats.mana}/{stats.Max_mana}";
        expTMP.text = $"{stats.CurrentExp}/{stats.NextLevelUp}";
        coinsTMP.text = CoinsManager.instance.Coins.ToString();
    }

    // Cập nhật thông tin panel chỉ số chi tiết
    private void UpdateStatsPanel()
    {
        statslv.text = stats.level.ToString();
        statsdmg.text = stats.TotalDmg.ToString();
        statscritc.text = stats.CritChance.ToString();
        statcritdmg.text = stats.CritDmg.ToString();
        statstotalexp.text = stats.TotalExp.ToString();
        statscurrentexp.text = stats.CurrentExp.ToString();
        statsreqExp.text = stats.NextLevelUp.ToString();
        attributepoint.text = $"Points: {stats.AttributePoint}";
        strength.text = stats.Strength.ToString();
        dexterity.text = stats.Dexterity.ToString();
        intelligence.text = stats.Intelligence.ToString();
    }

    // Xử lý tương tác mở panel theo loại
    private void ExtraInteractionCallback(InteractionType type)
    {
        switch (type)
        {
            case InteractionType.Quest:
                OpenCloseNPCQuestPanel(true);
                break;
            case InteractionType.Shop:
                OpenCloseShopPanel(true);
                break;
            case InteractionType.NormalTalk:
                break;
            case InteractionType.Crafting:
                OpenCloseCraftPanel(true);
                break;
        }
    }

    // Cập nhật panel chỉ số khi nâng cấp
    private void UpgradeCallback()
    {
        UpdateStatsPanel();
    }   

    // Đăng ký sự kiện nâng cấp và tương tác
    private void OnEnable()
    {
        PlayerUpdate.OnplayerUpgrade += UpgradeCallback;
        DialogManager.OnExtraInteractionEvent += ExtraInteractionCallback;
    }

    // Gỡ đăng ký sự kiện khi vô hiệu hóa
    private void OnDisable()
    {
        PlayerUpdate.OnplayerUpgrade -= UpgradeCallback;
        DialogManager.OnExtraInteractionEvent -= ExtraInteractionCallback;
    }
}