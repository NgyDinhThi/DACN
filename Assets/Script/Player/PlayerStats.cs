using UnityEngine;
using System;

// Cho phép tạo ScriptableObject từ menu Unity
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")]
    public int level; // Cấp độ của nhân vật

    [Header("Health info")] // Thông tin về máu của nhân vật
    public float health; // Máu hiện tại của nhân vật
    public float Max_health; // Máu tối đa của nhân vật
}
