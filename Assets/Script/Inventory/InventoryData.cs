using System;
using UnityEngine;

[Serializable]
// Lớp lưu trữ dữ liệu inventory, bao gồm nội dung item và số lượng của từng item
public class InventoryData
{
    public string[] itemsContents; // Mảng chứa tên các item trong inventory
    public int[] itemsQuantity; // Mảng chứa số lượng tương ứng của các item
}
