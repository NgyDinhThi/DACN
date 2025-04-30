using UnityEngine;
using System;


public enum LoaiVK
{
    Phep,
    CanChien
}


[CreateAssetMenu(fileName ="Vukhi_")]
/*
chỉ dùng được với class kế thừa từ ScriptableObject
 phải nằm dưới CreateAssetMenu
 */

public class Weapon : ScriptableObject
{ 
    [Header("Config")]

    public Sprite icon;
    public LoaiVK loaiVK;
    public float dmg;

    public Projectiles projectilesPrefab;

    public float luongMana;
}