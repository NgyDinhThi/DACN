using UnityEditor;
using UnityEngine;
using System;



[CustomEditor(typeof(PlayerStats))]

public class PlayerEditor : Editor
{
    private PlayerStats StatsTarget => target as PlayerStats;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Reset player"))
        {
            StatsTarget.ResetPlayer();
        }
    }
}
