using UnityEngine;
using System;
using UnityEngine.Rendering;

/// <summary>
/// Quản lý mana (năng lượng phép) cho nhân vật người chơi.
/// Bao gồm các chức năng như: sử dụng mana, hồi phục mana, và reset lại mana.
/// </summary>
public class PlayerMana : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    // Tham chiếu đến PlayerStats để lấy thông tin về lượng mana hiện tại và tối đa

    public float luongmn { get; private set; }
    // Thuộc tính công khai chỉ đọc để các class khác biết lượng mana hiện tại

    private void Start()
    {
        ResetMana(); // Khởi tạo: hồi lại đầy mana cho người chơi
    }

    private void Update()
    {
        // Không cần xử lý trong mỗi frame ở thời điểm hiện tại
    }

    /// <summary>
    /// Trừ mana theo lượng được truyền vào.
    /// Đảm bảo mana không nhỏ hơn 0.
    /// </summary>
    /// <param name="amount">Lượng mana cần sử dụng</param>
    public void UseMana(float amount)
    {
        stats.mana = Mathf.Max(stats.mana - amount, 0f); // Trừ mana và không để dưới 0
        luongmn = stats.mana; // Đồng bộ biến private với dữ liệu từ stats
    }

    /// <summary>
    /// Hồi lại toàn bộ mana lên mức tối đa.
    /// Dùng khi bắt đầu game hoặc hồi sinh.
    /// </summary>
    public void ResetMana()
    {
        luongmn = stats.Max_mana;
        stats.mana = stats.Max_mana;
    }

    /// <summary>
    /// Kiểm tra xem người chơi có thể hồi phục mana hay không.
    /// </summary>
    /// <returns>True nếu lượng mana nhỏ hơn mức tối đa</returns>
    public bool CanRecoverMana()
    {
        return stats.mana > 0 && stats.mana < stats.Max_mana;
    }

    /// <summary>
    /// Hồi phục một lượng mana nhất định.
    /// </summary>
    /// <param name="amount">Lượng mana cần hồi</param>
    public void RecoverMana(float amount)
    {
        stats.mana += amount;
        stats.mana = Mathf.Min(stats.mana, stats.Max_mana); // Không cho vượt quá giới hạn
        luongmn = stats.mana;
    }
}
