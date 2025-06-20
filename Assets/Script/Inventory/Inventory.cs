﻿using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using BayatGames.SaveGameFree;

public class Inventory : Singleton<Inventory>
{
    [Header("Header")]
    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItems[] inventoryItems;
    [SerializeField] private GameContents gameContents;

    [Header("Testing")]
    public InventoryItems testItem;

    public InventoryItems[] InventoryItems => inventoryItems;

    public int InventorySize => inventorySize;

    private readonly string INVENTORY_KEY_DATA = "PLAYER_INVENTORY";

    private void SaveInventory()
    {
        InventoryData saveData = new InventoryData();
        saveData.itemsContents = new string[inventorySize];
        saveData.itemsQuantity = new int[inventorySize];
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                saveData.itemsQuantity[i] = 0;
                saveData.itemsContents[i] = null;
            }
            else
            {
                saveData.itemsContents[i] = inventoryItems[i].Id;
                saveData.itemsQuantity[i] = inventoryItems[i].quantity;
            }
        }
        SaveGame.Save(INVENTORY_KEY_DATA, saveData);
    }

    private void Start()
    {
        inventoryItems = new InventoryItems[inventorySize];
        VerifiItems4Draw();
        LoadInventory();
    }

    private InventoryItems IsItemsExistInGamecontents(string itemsId)
    {
        for (int i = 0; i < gameContents.GameItems.Length; i++) 
        {
            if (gameContents.GameItems[i].Id == itemsId)
            {
                return gameContents.GameItems[i];
            }
        }
        return null;
    }

    private void LoadInventory()
    {
        if (SaveGame.Exists(INVENTORY_KEY_DATA))
        {
            InventoryData loadData = SaveGame.Load<InventoryData>(INVENTORY_KEY_DATA);
            for (int i = 0; i < inventorySize; i++)
            {
                if (loadData.itemsContents[i] != null)
                {
                    InventoryItems itemFromContents = IsItemsExistInGamecontents(loadData.itemsContents[i]);
                    if (itemFromContents != null)
                    {
                        inventoryItems[i] = itemFromContents.CopyItem();
                        inventoryItems[i].quantity = loadData.itemsQuantity[i]; // Gán quantity từ dữ liệu đã lưu
                        InventoryUI.instance.DrawItems(inventoryItems[i], i);
                    }
                }
                else
                {
                    inventoryItems[i] = null;
                    InventoryUI.instance.DrawItems(null, i);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItems(testItem, 1);
        }
    }

    public void AddItems(InventoryItems items, int quantity)
    {
        if (items == null || quantity <= 0) return;
        List<int> itemIndexes = CheckItemstockIndexes(items.Id);
        if (items.IsStackable && itemIndexes.Count > 0)
        {
            foreach (int index in itemIndexes)
            {
                int maxStack = items.MaxStack;
                if (inventoryItems[index].quantity < maxStack)
                {
                    inventoryItems[index].quantity += quantity;
                    if (inventoryItems[index].quantity > maxStack)
                    {
                        int dif = inventoryItems[index].quantity - maxStack;
                        inventoryItems[index].quantity = maxStack;
                        AddItemFreeSlot(items, dif);
                    }
                    InventoryUI.instance.DrawItems(inventoryItems[index], index);
                    SaveInventory();
                    return;
                }
            }
        }

        int quantityToAdd = quantity > items.MaxStack ? items.MaxStack : quantity;
        AddItemFreeSlot(items, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if (remainingAmount > 0)
        {
            AddItems(items, remainingAmount);
        }
        SaveInventory();
    }

    public void UseItems(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].UseItem())
        {
            DecreaseItemStack(index);
        }
        SaveInventory();
    }

    public void RemoveItems(int index)
    {
        if (inventoryItems[index] == null) return;
        inventoryItems[index].RemoveItem();
        inventoryItems[index] = null;
        InventoryUI.instance.DrawItems(null, index);
        SaveInventory();
    }

    public void EquipItems(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].itemtype != Itemtype.Weapon) return;
        inventoryItems[index].EquipItem();
        SaveInventory(); // Thêm SaveInventory để lưu trạng thái sau khi trang bị
    }

    private void AddItemFreeSlot(InventoryItems items, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null) continue;
            inventoryItems[i] = items.CopyItem();
            inventoryItems[i].quantity = quantity;
            InventoryUI.instance.DrawItems(inventoryItems[i], i);
            return;
        }
    }

    public void DecreaseItemStack(int index)
    {
        if (inventoryItems[index] == null) return;
        inventoryItems[index].quantity--;
        if (inventoryItems[index].quantity <= 0)
        {
            inventoryItems[index] = null;
            InventoryUI.instance.DrawItems(null, index);
        }
        else
        {
            InventoryUI.instance.DrawItems(inventoryItems[index], index);
        }
    }

    private List<int> CheckItemstockIndexes(string itemId)
    {
        List<int> itemIndexes = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].Id == itemId)
            {
                itemIndexes.Add(i);
            }
        }
        return itemIndexes;
    }

    public int GetItemsCurrentStock(string itemId)
    {
        List<int> indexes = CheckItemstockIndexes(itemId);
        int currentStock = 0;
        foreach (int index in indexes)
        {
            if (inventoryItems[index].Id == itemId)
            {
                currentStock += inventoryItems[index].quantity;
            }
        }
        return currentStock;
    }

    private void VerifiItems4Draw()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                InventoryUI.instance.DrawItems(null, i);
            }
        }
    }

    public void ConsumeItem(string itemId)
    {
        List<int> indexes = CheckItemstockIndexes(itemId);
        if (indexes.Count > 0)
        {
            DecreaseItemStack(indexes[^1]);
        }

    } 


}