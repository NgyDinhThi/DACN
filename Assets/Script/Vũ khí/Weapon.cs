// ScriptableObject lưu trữ thông tin cấu hình của vũ khí trong game
using UnityEngine;
using System;

public enum LoaiVK
{
    Phep,
    CanChien
}

[CreateAssetMenu(fileName = "Vukhi_", menuName = "Scriptable Objects/Vũ khí")]
public class Weapon : ScriptableObject
{
    [Header("Config")]
    public Sprite icon;
    public LoaiVK loaiVK;
    public float dmg;
    public Projectiles projectilesPrefab;
    public float requiredMana;
}