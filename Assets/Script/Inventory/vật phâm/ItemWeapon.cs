using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon", fileName = "ItemWeapon")]
// Lớp lưu trữ thông tin của một vũ khí trong game, kế thừa từ InventoryItems
public class ItemWeapon : InventoryItems
{
    [Header("Weapon")]
    public Weapon weapon; // Thông tin về vũ khí

    // Phương thức trang bị vũ khí cho người chơi
    public override void EquipItem()
    {
        WeaponManager.instance.EquipWeapon(weapon); // Trang bị vũ khí cho người chơi
    }
}
