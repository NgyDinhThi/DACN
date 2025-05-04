using UnityEngine;
using System;
using UnityEngine.Rendering;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private PlayerStats stats; // Tham chiếu đến chỉ số của nhân vật (bao gồm mana, max mana, v.v.)

    public float luongmn { get; private set; } // Thuộc tính công khai chỉ đọc: lưu lượng mana hiện tại để các class khác có thể truy cập

    private void Start()
    {
        ResetMana(); // Khi game bắt đầu, thiết lập mana đầy
    }

    private void Update()
    {

    }

    public void UseMana(float amount)
    {
        // Trừ mana nhưng đảm bảo không nhỏ hơn 0
        stats.mana = Mathf.Max(stats.mana - amount, 0f);
        luongmn = stats.mana; // Cập nhật lượng mana hiện tại để đồng bộ
    }

    public void ResetMana()
    {
        // Reset mana về mức tối đa (dùng khi nhân vật hồi sinh hoặc bắt đầu game)
        luongmn = stats.Max_mana;
    } 
}
