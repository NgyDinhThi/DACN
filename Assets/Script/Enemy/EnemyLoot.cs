using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Quản lý loot từ kẻ địch, bao gồm rơi vật phẩm và kinh nghiệm
public class EnemyLoot : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float expDrop;
    [SerializeField] private DropItem[] dropItems;

    public List<DropItem> Items { get; private set; }
    public float ExpDrop => expDrop;

    // Phương thức được gọi khi bắt đầu, tải danh sách vật phẩm có thể rơi từ kẻ địch
    private void Start()
    {
        LoadDropItems();
    }

    // Phương thức để xác định các vật phẩm có thể rơi, dựa trên tỷ lệ rơi
    private void LoadDropItems()
    {
        Items = new List<DropItem>();
        foreach (DropItem item in dropItems)
        {
            float prob = Random.Range(0f, 100f);
            if (prob <= item.DropChance)
            {
                Items.Add(item);
            }
        }
        Debug.Log($"Loaded {Items.Count} items in EnemyLoot");
    }
}

[Serializable]
public class DropItem
{
    public string Name;
    public InventoryItems Item;
    public int Quantity;
    public float DropChance;
    public bool PickedItem { get; set; }
}
