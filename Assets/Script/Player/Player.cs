using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private PlayerStats stats; // Tham chiếu đến thông tin chỉ số của nhân vật

    public PlayerMana playerMana {  get; private set; }

    public PlayerHealth playerhealth { get; private set; }



    // Thuộc tính cho phép truy xuất chỉ số của nhân vật từ bên ngoài
    public PlayerStats Stats => stats;

    private PlayerAnimation playerAnimation; // Tham chiếu đến script điều khiển animation của nhân vật

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>(); // Lấy component PlayerAnimation từ GameObject
        playerhealth = GetComponent<PlayerHealth>();
        playerMana = GetComponent<PlayerMana>();  // Lấy script quản lý mana
    }

    public void ResetPlayer()
    {
        // Reset lại chỉ số và trạng thái animation của nhân vật
        stats.ResetPlayer(); // Gọi hàm reset stats trong ScriptableObject PlayerStats
        playerAnimation.ResetPlayer(); // Gọi hàm reset animation trạng thái trong PlayerAnimation
        playerMana.ResetMana(); // Hồi đầy mana cho nhân vật

    }
}
