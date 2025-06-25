using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Quản lý các ô trong inventory
public class InventorySlot : MonoBehaviour
{
    public static event Action<int> OnSlotSelectedEvent;

    [Header("Config")]
    [SerializeField] private Image itemsIcons;
    [SerializeField] private Image quantityContainer;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;

    public int Index { get; set; }

    // Không thực hiện gì trong phương thức này
    private void Start() { }

    // Kích hoạt sự kiện khi nhấn vào ô, truyền chỉ số ô
    public void ClickSlot()
    {
        OnSlotSelectedEvent?.Invoke(Index);
    }

    // Cập nhật icon và số lượng của item trong ô
    public void UpdateSlot(InventoryItems items)
    {
        itemsIcons.sprite = items.Icon;
        itemQuantityTMP.text = items.quantity.ToString();
        itemsIcons.SetNativeSize();
    }

    // Hiển thị hoặc ẩn icon và số lượng của item
    public void ShowSlotInfo(bool value)
    {
        itemsIcons.gameObject.SetActive(value);
        quantityContainer.gameObject.SetActive(value);
    }
}
