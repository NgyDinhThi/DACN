using System;
using UnityEngine;

// Quản lý cửa hàng, khởi tạo và hiển thị các thẻ vật phẩm
public class ShopManager : Singleton<ShopManager>
{
    [Header("Config")]
    [SerializeField] private ShopCard shopCardPrefab; // Mẫu thẻ vật phẩm
    [SerializeField] private Transform shopContainer; // Container chứa các thẻ vật phẩm

    [Header("Items")]
    [SerializeField] private ShopItem[] items; // Danh sách vật phẩm trong cửa hàng

    // Khởi tạo cửa hàng khi bắt đầu
    private void Start()
    {
        LoadShop();
    }

    // Tạo và cấu hình các thẻ vật phẩm trong cửa hàng
    private void LoadShop()
    {
        for (int i = 0; i < items.Length; i++)
        {
            ShopCard card = Instantiate(shopCardPrefab, shopContainer);
            card.ConfigShopCard(items[i]);
        }
    }
}