using UnityEngine;
using System;
//quản lý mana của người chơi
public class PlayerMana : MonoBehaviour
{
    [SerializeField] private PlayerStats stats; // Tham chiếu đến dữ liệu mana của người chơi
    public float luongmn { get; private set; } // Mana hiện tại

    private void Start()
    {
        ResetMana(); // Hồi đầy mana khi bắt đầu
    }

    private void Update()
    {
        
    }

    public void UseMana(float amount)
    {
        // Trừ mana
        stats.mana = Mathf.Max(stats.mana - amount, 0f);
        luongmn = stats.mana;
    }

    public void ResetMana()
    {
        // Hồi đầy mana
        luongmn = stats.Max_mana;
        stats.mana = stats.Max_mana;
    }

    public bool CanRecoverMana()
    {
        // Kiểm tra có thể hồi mana
        return stats.mana > 0 && stats.mana < stats.Max_mana;
    }

    public void RecoverMana(float amount)
    {
        // Hồi một lượng mana
        stats.mana += amount;
        stats.mana = Mathf.Min(stats.mana, stats.Max_mana);
        luongmn = stats.mana;
    }
}
