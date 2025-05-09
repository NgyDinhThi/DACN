using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic; 

public class Inventory : Singleton<Inventory>
{
    [Header("Header")]

    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItems[] inventoryItems; 
    
    public int InventorySize => inventorySize;

    public void Start()
    {
        inventoryItems = new InventoryItems[inventorySize];
    }

    private void Update()
    {
        
    }

    public void AddItems(InventoryItems items, int quantity)
    {
        if (items == null && quantity <= 0) return;
        List<int> itemIndexes = CheckItemstock(items.id);
        if (items.IsStackable && itemIndexes.Count > 0)
        {
            foreach (int index in itemIndexes)
            {
                int maxStack = items.MaxStack;
                if (inventoryItems[index].quantity< maxStack)
                {
                    inventoryItems[index].quantity += quantity;
                    if (inventoryItems[index].quantity > maxStack)
                    {
                        int dif = inventoryItems[index].quantity - maxStack;
                        inventoryItems[index].quantity = maxStack;
                        AddItems(items, dif);
                    }

                    InventoryUI.instance.DrawItems(inventoryItems[index], index);
                    return;
                }
            }
        }
    }    

    private void AddItemFreeSlot(InventoryItems items, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null)
                continue;
            inventoryItems[i] = items.CopyItem();
            inventoryItems[i].quantity = quantity;
            InventoryUI.instance.DrawItems(inventoryItems[i], i);
        }

    }    

    private List<int> CheckItemstock(string itemId)
    {
        List<int> itemIndexes = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].id == itemId)
            {
                itemIndexes.Add(i);
            }
        }
        return itemIndexes;

    }    

}
