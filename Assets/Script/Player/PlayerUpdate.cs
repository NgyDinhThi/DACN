using System;
using UnityEngine;

// Quản lý nâng cấp chỉ số của người chơi dựa trên điểm thuộc tính
public class PlayerUpdate : MonoBehaviour
{
    public static event Action OnplayerUpgrade;

    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    [Header("Setting")]
    [SerializeField] private UpgradeSetting[] settings;

    // Nâng cấp các chỉ số của người chơi dựa trên chỉ số nâng cấp
    private void UpgradePlayer(int upgradeIndex)
    {
        stats.BaseDmg += settings[upgradeIndex].DmgUpgrade;
        stats.TotalDmg += settings[upgradeIndex].DmgUpgrade;
        stats.Max_health += settings[upgradeIndex].HealthUpgrade;
        stats.health = stats.Max_health;
        stats.Max_mana += settings[upgradeIndex].ManaUpgrade;
        stats.mana = stats.Max_mana;
        stats.CritChance += settings[upgradeIndex].CritchanceUpgrade;
        stats.CritDmg += settings[upgradeIndex].CritdmgUpgrade;
    }

    // Xử lý nâng cấp thuộc tính khi nhận sự kiện từ nút thuộc tính
    private void AttributeCallback(Attribute attribute)
    {
        if (stats.AttributePoint == 0) return;
        switch (attribute)
        {
            case Attribute.Strength:
                UpgradePlayer(0);
                stats.Strength++;
                break;
            case Attribute.Dexterity:
                UpgradePlayer(1);
                stats.Dexterity++;
                break;
            case Attribute.Intelligence:
                UpgradePlayer(2);
                stats.Intelligence++;
                break;
        }

        stats.AttributePoint--;
        OnplayerUpgrade?.Invoke();
    }

    // Đăng ký sự kiện nâng cấp thuộc tính khi kích hoạt
    private void OnEnable()
    {
        AttributeButton.OnAttributeEvent += AttributeCallback;
    }

    // Hủy đăng ký sự kiện nâng cấp thuộc tính khi vô hiệu hóa
    private void OnDisable()
    {
        AttributeButton.OnAttributeEvent -= AttributeCallback;
    }
}

// Lưu trữ thông tin nâng cấp cho mỗi loại thuộc tính
[Serializable]
public class UpgradeSetting
{
    public string Name;

    [Header("Value")]
    public float DmgUpgrade;
    public float HealthUpgrade;
    public float ManaUpgrade;
    public float CritchanceUpgrade;
    public float CritdmgUpgrade;
}