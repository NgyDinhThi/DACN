using UnityEngine;

/// <summary>
/// Đại diện cho một bình hồi mana trong hệ thống item.
/// Kế thừa từ InventoryItems và ghi đè lại hành vi khi sử dụng.
/// </summary>
[CreateAssetMenu(fileName = "ItemManaPotion", menuName = "Items/Mana potion")]
public class ItemManaPotion : InventoryItems
{
    [Header("Config")]
    public float ManaValue; // Lượng mana mà bình thuốc này sẽ hồi

    /// <summary>
    /// Hành động khi sử dụng bình mana.
    /// Nếu người chơi có thể hồi mana, sẽ cộng mana và trả về true.
    /// Ngược lại, không làm gì và trả về false.
    /// </summary>
    /// <returns>true nếu sử dụng thành công, false nếu không.</returns>
    public override bool UseItem()
    {
        // Kiểm tra nếu người chơi có thể hồi mana
        if (GameManager.instance.Player.playerMana.CanRecoverMana())
        {
            // Gọi hàm hồi mana trong PlayerMana
            GameManager.instance.Player.playerMana.RecoverMana(ManaValue);
            return true; // Sử dụng thành công
        }

        return false; // Không thể sử dụng (đã đầy mana)
    }
}
