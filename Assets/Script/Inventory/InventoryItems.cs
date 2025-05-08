using UnityEngine;

// Định nghĩa các loại vật phẩm có thể có trong game
public enum Itemtype
{
    Weapon,        // Vũ khí
    Potion,        // Thuốc hồi máu/mana
    Scroll,        // Bùa chú (kỹ năng phép thuật)
    Ingredients,   // Nguyên liệu chế tạo
    Treasure       // Vật phẩm giá trị cao, có thể bán hoặc dùng
}

// Cho phép tạo asset ScriptableObject từ menu Unity: Assets > Create > Items > Item
[CreateAssetMenu(menuName = "Items/Item")]
public class InventoryItems : ScriptableObject
{
    [Header("Config")]
    public string id;             // ID định danh duy nhất của item
    public string name;           // Tên hiển thị của item
    public Sprite icon;           // Icon hiển thị của item trong giao diện
    [TextArea] public string mieuta; // Mô tả chi tiết về item

    [Header("Info")]
    public Itemtype itemtype;     // Loại item
    public bool IsComsumable;     // Có thể tiêu thụ không (như Potion)
    public bool IsStackable;      // Có thể xếp chồng không (như thuốc hoặc nguyên liệu)
    public int MaxStack;          // Số lượng tối đa có thể chứa trong một ô (nếu stack được)

    [HideInInspector] public int Quantity; // Số lượng hiện có của item (ẩn trong inspector, dùng trong runtime)

    // Tạo một bản sao item (thường dùng khi thêm vào inventory từ prefab gốc)
    public InventoryItems CopyItem()
    {
        InventoryItems instance = Instantiate(this);
        return instance;
    }

    // Gọi khi sử dụng item (có thể override bởi item con)
    public virtual bool UseItem()
    {
        return true;
    }

    // Gọi khi trang bị item (override nếu là vũ khí/giáp...)
    public virtual void EquipItem()
    {
        // Mặc định không làm gì
    }

    // Gọi khi loại bỏ item khỏi inventory
    public virtual void RemoveItem()
    {
        // Mặc định không làm gì
    }
}
