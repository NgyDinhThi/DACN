using UnityEngine;
using System;

// Enum xác định loại vũ khí: Phép thuật hoặc Cận chiến
public enum LoaiVK
{
    Phep,      // Dùng để bắn đạn phép (cần mana)
    CanChien   // Tấn công gần, ví dụ như kiếm, rìu...
}

// Cho phép tạo asset ScriptableObject từ menu Unity (Assets > Create > ...)
[CreateAssetMenu(fileName = "Vukhi_", menuName = "Scriptable Objects/Vũ khí")]
/*
  Chỉ dùng được với class kế thừa từ ScriptableObject
  Phải nằm ngay trước class để Unity hiển thị đúng trên menu tạo asset
*/
public class Weapon : ScriptableObject
{
    [Header("Config")]

    public Sprite icon; // Icon đại diện của vũ khí (hiển thị trong UI)
    public LoaiVK loaiVK; // Loại vũ khí (Phep hoặc CanChien)
    public float dmg; // Sát thương gây ra khi tấn công

    public Projectiles projectilesPrefab; // Prefab viên đạn nếu là vũ khí tầm xa (phép)

    public float requiredMana; // Lượng mana tiêu hao mỗi khi sử dụng (nếu là phép)
}
