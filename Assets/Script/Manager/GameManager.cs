using UnityEngine;
using System;

/// <summary>
/// Quản lý các chức năng chính của game như kinh nghiệm, reset player.
/// Kế thừa từ Singleton để đảm bảo chỉ tồn tại một GameManager duy nhất trong toàn bộ game.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player player; // Tham chiếu đến đối tượng Player trong game (gắn sẵn từ Inspector)

    // Thuộc tính công khai để lấy thông tin Player hiện tại
    public Player Player => player;

    /// <summary>
    /// Gọi khi muốn tăng kinh nghiệm cho nhân vật.
    /// </summary>
    /// <param name="expamount">Lượng kinh nghiệm muốn cộng thêm.</param>
    public void AddPlayerExp(float expamount)
    {
        PlayerExp playerExp = player.GetComponent<PlayerExp>(); // Lấy component PlayerExp từ đối tượng Player
        playerExp.AddExp(expamount); // Thêm EXP vào nhân vật
    }

    private void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím R
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ResetPlayer(); // Gọi hàm ResetPlayer để khôi phục trạng thái ban đầu của nhân vật
        }
    }
}
