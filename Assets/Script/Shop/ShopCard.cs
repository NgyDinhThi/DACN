using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Quản lý hiển thị và tương tác mua vật phẩm trong cửa hàng
public class ShopCard : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCost;
    [SerializeField] private TextMeshProUGUI buyAmount;

    private ShopItem item;
    private int quantity;
    private float initialCost;
    private float currentCost;

    // Khởi tạo card với thông tin vật phẩm
    public void ConfigShopCard(ShopItem shopItem)
    {
        item = shopItem;
        itemIcon.sprite = shopItem.Item.Icon;
        itemName.text = shopItem.Item.ItemsName;
        itemCost.text = shopItem.Cost.ToString();
        quantity = 1;
        initialCost = shopItem.Cost;
        currentCost = shopItem.Cost;
    }

    // Cập nhật số lượng và giá hiển thị
    private void Update()
    {
        buyAmount.text = quantity.ToString();
        itemCost.text = currentCost.ToString();
    }

    // Tăng số lượng vật phẩm nếu đủ tiền
    public void Add()
    {
        float buycost = initialCost * (quantity + 1);
        if (CoinsManager.instance.Coins >= buycost)
        {
            quantity++;
            currentCost = initialCost * quantity;
        }
    }

    // Giảm số lượng vật phẩm, không giảm nếu bằng 1
    public void Remove()
    {
        if (quantity == 1) return;
        quantity--;
        currentCost = initialCost * quantity;
    }

    // Mua vật phẩm, thêm vào kho và trừ tiền
    public void BuyItems()
    {
        if (CoinsManager.instance.Coins >= currentCost)
        {
            Inventory.instance.AddItems(item.Item, quantity);
            CoinsManager.instance.RemoveCoin(currentCost);
            quantity = 1;
            currentCost = initialCost;
        }
    }
}