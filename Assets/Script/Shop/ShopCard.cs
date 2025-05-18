using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void Update()
    {
        buyAmount.text = quantity.ToString();
        itemCost.text = currentCost.ToString();
    }

    public void Add()
    {
        float buycost = initialCost *(quantity + 1);
        if (CoinsManager.instance.Coins >= buycost)
        {
            quantity++;
            currentCost = initialCost * quantity;
        }
    }

    public void Remove()
    {
        if (quantity == 1) return;
        quantity--;
        currentCost = initialCost * quantity;

    }


}