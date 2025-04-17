using UnityEngine;
using System;

public class PlayerExp : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats; // Tham chiếu đến ScriptableObject chứa chỉ số người chơi

    private void Update()
    {
        // Kiểm tra nếu phím X được nhấn, nhân vật sẽ nhận 200 EXP
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddExp(200f); // Thêm 200 điểm kinh nghiệm
        }
    }

    // Hàm thêm kinh nghiệm cho nhân vật
    public void AddExp(float Quantity)
    {
        stats.CurrentExp += Quantity; // Cộng thêm lượng EXP vào chỉ số hiện tại

        // Nếu đủ EXP để lên cấp, thực hiện quá trình lên cấp
        while (stats.CurrentExp >= stats.NextLevelUp)
        {
            stats.CurrentExp -= stats.NextLevelUp; // Trừ EXP đã dùng để lên cấp
            NewLevelGrow(); // Gọi hàm tăng cấp
        }
    }

    // Hàm xử lý khi nhân vật lên cấp mới
    private void NewLevelGrow()
    {
        stats.level++; // Tăng cấp độ nhân vật

        float currentExpRequired = stats.NextLevelUp; // Lưu EXP cần thiết hiện tại

        // Tính toán EXP cần thiết cho cấp tiếp theo dựa trên phần trăm hệ số tăng trưởng
        float newNextLevelUp = MathF.Round(currentExpRequired + stats.NextLevelUp * (stats.ExpMultiplier / 100f));

        stats.NextLevelUp = newNextLevelUp; // Cập nhật EXP cần thiết cho cấp tiếp theo
    }
}