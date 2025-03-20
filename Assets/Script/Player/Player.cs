using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("Config")] // Hiển thị tiêu đề "Config" trong Inspector
    [SerializeField] private PlayerStats stats; // Tham chiếu đến thông tin chỉ số của nhân vật


    public PlayerStats Stats => stats;

}