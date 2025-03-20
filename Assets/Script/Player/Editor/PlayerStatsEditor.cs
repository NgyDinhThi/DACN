using UnityEditor;
using UnityEngine;
using System;

// Tùy chỉnh giao diện của PlayerStats trong Inspector
[CustomEditor(typeof(PlayerStats))]
public class PlayerEditor : Editor
{
    // Lấy đối tượng PlayerStats hiện tại
    private PlayerStats StatsTarget => target as PlayerStats;

    // Ghi đè phương thức vẽ giao diện Inspector
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Vẽ giao diện mặc định

        // Tạo một nút trong Inspector
        if (GUILayout.Button("Reset player"))
        {
            StatsTarget.ResetPlayer(); // Gọi phương thức ResetPlayer khi nhấn nút
        }
    }
}