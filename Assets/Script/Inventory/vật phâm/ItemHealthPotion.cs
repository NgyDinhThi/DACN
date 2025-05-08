using UnityEngine;

/// <summary>
/// ScriptableObject đại diện cho item "bình hồi máu" trong game.
/// Kế thừa từ InventoryItems và định nghĩa hành vi khi sử dụng item.
/// </summary>
[CreateAssetMenu(fileName = "ItemHealthPotion", menuName = "Items/Health potion")]
public class ItemHealthPotion : InventoryItems
{
    [Header("Config")]
    public float Healthvalue; // Giá trị máu được hồi khi dùng bình

    /// <summary>
    /// Ghi đè hàm UseItem từ InventoryItems.
    /// Nếu nhân vật có thể hồi máu thì hồi máu và trả về true.
    /// Nếu không thể hồi (máu đầy) thì trả về false.
    /// </summary>
    /// <returns>True nếu dùng được, ngược lại là false</returns>
    public override bool UseItem()
    {
        // Kiểm tra điều kiện hồi máu
        if (GameManager.instance.Player.playerhealth.CanRestoreHealth())
        {
            // Hồi máu cho nhân vật
            GameManager.instance.Player.playerhealth.RestoredHealth(Healthvalue);
            return true;
        }

        // Nếu không thể hồi máu (ví dụ như máu đã đầy)
        return false;
    }
}
