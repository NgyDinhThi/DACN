using UnityEngine;


[CreateAssetMenu(menuName ="Items/Weapon ", fileName ="ItemWeapon")]
public class ItemWeapon : InventoryItems
{
    [Header("Weapon")]

    public Weapon weapon;

    public override void EquipItem()
    {
        WeaponManager.instance.EquipWeapon(weapon);
    }
}
