using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Singleton<WeaponManager>
{
    [Header("Config")]
    [SerializeField] private Image weaponIcons;
    [SerializeField] private TextMeshProUGUI weaponManaTMP;

    // Trang bị vũ khí mới cho người chơi và cập nhật UI
    public void EquipWeapon(Weapon weapon)
    {
        weaponIcons.sprite = weapon.icon;
        weaponIcons.SetNativeSize();
        weaponIcons.gameObject.SetActive(true);
        weaponManaTMP.text = weapon.requiredMana.ToString();
        weaponManaTMP.gameObject.SetActive(true);
        GameManager.instance.Player.playerAttack.EquipWeapon(weapon);
    }
}