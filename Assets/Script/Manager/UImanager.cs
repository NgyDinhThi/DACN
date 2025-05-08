using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStats stats; // Tham chiếu đến scriptable object chứa chỉ số của nhân vật

    [Header("Stats bars")]
    [SerializeField] private Image healthBar; // Thanh hiển thị lượng máu hiện tại
    [SerializeField] private Image manaBar;   // Thanh hiển thị lượng mana hiện tại
    [SerializeField] private Image expBar;    // Thanh hiển thị lượng EXP hiện tại

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI healthTMP; // Text hiển thị số máu
    [SerializeField] private TextMeshProUGUI levelTMP;  // Text hiển thị cấp độ
    [SerializeField] private TextMeshProUGUI manaTMP;   // Text hiển thị số mana
    [SerializeField] private TextMeshProUGUI expTMP;    // Text hiển thị EXP

    [Header("Stats Panel")] // Các chỉ số chi tiết mở rộng
    [SerializeField] private GameObject statsPanel; // Panel thông tin chi tiết
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

    private void Update()
    {
        UpdatePlayerUI(); // Cập nhật liên tục giao diện thanh máu, mana, exp, cấp độ
    }

    // Mở/đóng panel chỉ số nhân vật
    public void OpenCloseStsPanel()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
        if (statsPanel.activeSelf)
            UpdateStatsPanel(); // Nếu panel bật thì cập nhật dữ liệu mới
    }

    // Cập nhật giao diện thanh trạng thái chính
    private void UpdatePlayerUI()
    {
        // Làm mượt giá trị hiển thị bằng Lerp
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, stats.health / stats.Max_health, Time.deltaTime * 10f);
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, stats.mana / stats.Max_mana, Time.deltaTime * 10f);
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, stats.CurrentExp / stats.NextLevelUp, Time.deltaTime * 10f);

        // Cập nhật văn bản tương ứng
        healthTMP.text = $"{stats.health}/{stats.Max_health}";
        levelTMP.text = $"Level {stats.level}";
        manaTMP.text = $"{stats.mana}/{stats.Max_mana}";
        expTMP.text = $"{stats.CurrentExp}/{stats.NextLevelUp}";
    }

    // Cập nhật panel chỉ số mở rộng khi mở ra
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

    // Gọi lại hàm update panel khi có sự kiện nâng cấp
    private void UpgradeCallback()
    {
        UpdateStatsPanel();
    }

    private void OnEnable()
    {
        PlayerUpdate.OnplayerUpgrade += UpgradeCallback; // Đăng ký lắng nghe sự kiện nâng cấp chỉ số
    }

    private void OnDisable()
    {
        PlayerUpdate.OnplayerUpgrade -= UpgradeCallback; // Gỡ đăng ký khi bị disable
    }
}
