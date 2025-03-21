using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("Config")] // Hiển thị tiêu đề "Config" trong Inspector
    [SerializeField] private PlayerStats stats; // Tham chiếu đến thông tin chỉ số của nhân vật

    // Thuộc tính cho phép truy xuất chỉ số của nhân vật từ bên ngoài
    public PlayerStats Stats => stats;

    private PlayerAnimation animation;

    private void Awake()
    {
        animation = GetComponent<PlayerAnimation>();
    }

    public void ResetPlayer()
    {
        //reset lại mọi thứ
        stats.ResetPlayer();
        animation.ResetPlayer();
    }
}