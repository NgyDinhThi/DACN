using UnityEngine;
using System;

// Lớp Player dùng để gom các thành phần liên quan đến nhân vật người chơi (máu, mana, animation, stats)
public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats; // Tham chiếu đến ScriptableObject chứa chỉ số nhân vật

    // Thuộc tính công khai giúp truy cập thành phần mana và máu
    public PlayerMana playerMana { get; private set; } // Quản lý lượng mana hiện tại
    public PlayerHealth playerHealth { get; private set; } // Quản lý lượng máu hiện tại

    public PlayerAttack playerAttack { get; private set; }

    // Cho phép các script khác truy cập thông tin chỉ số nhân vật
    public PlayerStats Stats => stats;

    private PlayerAnimation playerAnimation; // Điều khiển animation di chuyển, chết, tấn công,...

    private void Awake()
    {
        // Lấy các component cần thiết trên GameObject nhân vật
        playerAnimation = GetComponent<PlayerAnimation>(); // Gắn script điều khiển animation
        playerHealth = GetComponent<PlayerHealth>();       // Gắn script quản lý máu
        playerMana = GetComponent<PlayerMana>();           // Gắn script quản lý mana
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Hàm reset trạng thái nhân vật về ban đầu (gọi khi hồi sinh, chơi lại,...)
    public void ResetPlayer()
    {
        stats.ResetPlayer();         // Reset lại chỉ số máu, mana, cấp độ,...
        playerAnimation.ResetPlayer(); // Reset lại animation về trạng thái ban đầu
        playerMana.ResetMana();      // Hồi đầy mana
    }
}
