using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using BayatGames.SaveGameFree;

// Quản lý inventory của người chơi, bao gồm thêm, sử dụng, xóa, trang bị item, và lưu trữ dữ liệu inventory
public class Inventory : Singleton<Inventory>
{
    [Header("Header")]
    [SerializeField] private int inventorySize; // Kích thước của inventory
    [SerializeField] public InventoryItems[] inventoryItems; // Danh sách các item trong inventory
    [SerializeField] private GameContents gameContents; // Thông tin về các item có sẵn trong game

    [Header("Testing")]
    public InventoryItems testItem; // Item dùng để kiểm tra trong quá trình phát triển

    public InventoryItems[] InventoryItems => inventoryItems;
    public int InventorySize => inventorySize;

    private readonly string INVENTORY_KEY_DATA = "PLAYER_INVENTORY";

    // Lưu trữ dữ liệu inventory vào hệ thống lưu trữ
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
        SaveGame.Save(INVENTORY_KEY_DATA, saveData); // Lưu lại dữ liệu vào game
    }

    // Khởi tạo inventory và tải dữ liệu từ bộ nhớ
    private void Start()
    {
        inventoryItems = new InventoryItems[inventorySize];
        VerifiItems4Draw();
        LoadInventory(); // Tải dữ liệu inventory từ bộ nhớ
    }

    // Kiểm tra nếu item tồn tại trong game contents
    public InventoryItems IsItemsExistInGamecontents(string itemsId)
    {
        for (int i = 0; i < gameContents.GameItems.Length; i++)
        {
            if (gameContents.GameItems[i].Id == itemsId)
            {
                return gameContents.GameItems[i]; // Trả về item nếu tìm thấy
            }
        }
        return null; // Trả về null nếu không tìm thấy
    }

    // Tải dữ liệu inventory từ bộ nhớ
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
                        inventoryItems[i].quantity = loadData.itemsQuantity[i];
                        InventoryUI.instance.DrawItems(inventoryItems[i], i); // Vẽ item lên UI
                    }
                }
                else
                {
                    inventoryItems[i] = null;
                    InventoryUI.instance.DrawItems(null, i); // Vẽ ô trống trên UI
                }
            }
        }
    }

    // Cập nhật mỗi khung hình nếu người dùng nhấn phím H để thêm item
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItems(testItem, 1);
        }
    }

    // Thêm item vào inventory, xử lý khi item có thể stack
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

    // Sử dụng item trong inventory
    public void UseItems(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].UseItem())
        {
            DecreaseItemStack(index);
        }
        SaveInventory();
    }

    // Xóa item khỏi inventory
    public void RemoveItems(int index)
    {
        if (inventoryItems[index] == null) return;
        inventoryItems[index].RemoveItem();
        inventoryItems[index] = null;
        InventoryUI.instance.DrawItems(null, index);
        SaveInventory();
    }

    // Trang bị item cho người chơi
    public void EquipItems(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].itemtype != Itemtype.Weapon) return;
        inventoryItems[index].EquipItem();
        SaveInventory();
    }

    // Thêm item vào slot trống trong inventory
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

    // Giảm số lượng stack của item trong inventory
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

    // Kiểm tra các ô có chứa item trùng với itemId
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

    // Lấy số lượng item hiện tại trong inventory
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

    // Kiểm tra và vẽ lại các ô trống trong inventory
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

    // Tiêu thụ item trong inventory
    public void ConsumeItem(string itemId)
    {
        List<int> indexes = CheckItemstockIndexes(itemId);
        if (indexes.Count > 0)
        {
            DecreaseItemStack(indexes[^1]);
        }
    }

    public void ResetInventory()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i] = null;
            InventoryUI.instance.DrawItems(null, i); // Vẽ ô trống
        }

        SaveGame.Delete("PLAYER_INVENTORY"); // Xoá file lưu cũ (nếu dùng Bayat SaveGame)
    }

}
