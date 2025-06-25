
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private GameObject lootPanel;
    [SerializeField] private LootButton lootButtonPrefab;
    [SerializeField] private Transform container;

    // Hiển thị panel loot và sinh các nút nhặt đồ tương ứng với danh sách drop
    public void ShowLoot(EnemyLoot enemyLoot)
    {
        lootPanel.SetActive(true);

        // Nếu panel đang có item thì xóa toàn bộ
        if (LootPanelWithItems())
        {
            for (int i = 0; i < container.childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }

        // Duyệt qua từng vật phẩm drop và tạo loot button tương ứng
        foreach (DropItem item in enemyLoot.Items)
        {
            if (item.PickedItem) continue;
            Debug.Log($"Processing item: Name={item.Name}, Item={item.Item}, Quantity={item.Quantity}, Picked={item.PickedItem}");
            LootButton lootButton = Instantiate(lootButtonPrefab, container);
            lootButton.ConfigLootButton(item);
        }
    }

    // Đóng panel loot
    public void ClosePanel()
    {
        lootPanel.SetActive(false);
    }

    // Kiểm tra panel hiện tại có đang chứa item không
    private bool LootPanelWithItems()
    {
        return container.childCount > 0;
    }
}
