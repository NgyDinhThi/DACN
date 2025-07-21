using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Singleton<WeaponManager>
{
    [Header("Config")]
    [SerializeField] private Image weaponIcons;
    [SerializeField] private TextMeshProUGUI weaponManaTMP;
    [Header("Game Content")]
    [SerializeField] private GameContents gameContents;
    [SerializeField] private Weapon[] gameWeapons;

    public Weapon currentWeapon { get; private set; }


    // Trang bị vũ khí mới cho người chơi và cập nhật UI
    public void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        weaponIcons.sprite = weapon.icon;
        weaponIcons.SetNativeSize();
        weaponIcons.gameObject.SetActive(true);
        weaponManaTMP.text = weapon.requiredMana.ToString();
        weaponManaTMP.gameObject.SetActive(true);
        GameManager.instance.Player.playerAttack.EquipWeapon(weapon);
    }

    public void LoadWeaponByName(string weaponName)
    {
        if (string.IsNullOrEmpty(weaponName)) return;

        foreach (Weapon weapon in gameWeapons)
        {
            if (weapon != null && weapon.name == weaponName)
            {
                EquipWeapon(weapon);
                Debug.Log($"Đã load weapon: {weaponName}");
                return;
            }
        }

        Debug.LogWarning($"Không tìm thấy vũ khí có tên: {weaponName} trong GameWeapons.");
    }

}