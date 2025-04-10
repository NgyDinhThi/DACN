using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStats stats; // Tham chiếu đến chỉ số của người chơi

    [Header("Stats bars")]
    [SerializeField] private Image healthBar; // Thanh hiển thị máu
    [SerializeField] private Image manaBar;   // Thanh hiển thị mana
    [SerializeField] private Image expBar;    // Thanh hiển thị kinh nghiệm

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI healthTMP; // Text hiển thị máu
    [SerializeField] private TextMeshProUGUI levelTMP;  // Text hiển thị cấp độ
    [SerializeField] private TextMeshProUGUI manaTMP;   // Text hiển thị mana
    [SerializeField] private TextMeshProUGUI expTMP;    // Text hiển thị kinh nghiệm

    private void Update()
    {
        UpdatePlayerUI(); // Cập nhật giao diện người chơi mỗi frame
    }

    private void UpdatePlayerUI()
    {
        // Làm mượt và cập nhật thanh máu dựa trên tỉ lệ hiện tại và tối đa
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, stats.health / stats.Max_health, Time.deltaTime * 10f);

        // Làm mượt và cập nhật thanh mana
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, stats.mana / stats.Max_mana, Time.deltaTime * 10f);

        // Làm mượt và cập nhật thanh kinh nghiệm
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, stats.CurrentExp / stats.NextLevelUp, Time.deltaTime * 10f);

        // Cập nhật text số liệu máu, mana, cấp độ và kinh nghiệm
        healthTMP.text = $"{stats.health}/{stats.Max_health}";
        levelTMP.text = $"Level {stats.level}";
        manaTMP.text = $"{stats.mana}/{stats.Max_mana}";
        expTMP.text = $"{stats.CurrentExp}/{stats.NextLevelUp}";
    }
}
