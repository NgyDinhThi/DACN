using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Config")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Image itemsIcon;
    [SerializeField] private TextMeshProUGUI itemsNameTMP;
    [SerializeField] private TextMeshProUGUI itemsDescriptionTMP;

    public InventorySlot CurrentSlot { get; set; }

    private List<InventorySlot> slotList = new List<InventorySlot>();

    protected override void Awake()
    {
        base.Awake();
        ClearExistingSlots(); // Xóa các ô thừa trước khi khởi tạo
        InitInventory();
    }

    private void Start()
    {
        UpdateInventoryDisplay();
    }

    private void ClearExistingSlots()
    {
        // Xóa tất cả các ô hiện có trong container
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        slotList.Clear(); // Xóa danh sách slot cũ
    }

    private void InitInventory()
    {
        // Giới hạn số ô tối đa là 24
        int maxSlots = Mathf.Min(24, Inventory.instance.InventorySize);
        for (int i = 0; i < maxSlots; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.Index = i;
            slotList.Add(slot);
            // Đảm bảo mỗi ô có Button và sự kiện nhấp
            Button button = slot.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => slot.ClickSlot());
            }
            else
            {
                Debug.LogWarning($"Button missing on slot at index {i}");
            }
        }
        Debug.Log($"Đã khởi tạo {slotList.Count} ô trong kho đồ.");
    }

    public void UpdateInventoryDisplay()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            InventoryItems item = Inventory.instance.InventoryItems[i];
            DrawItems(item, i);
        }
    }

    public void UseItems()
    {
        if (CurrentSlot == null) return;
        Inventory.instance.UseItems(CurrentSlot.Index);
        UpdateInventoryDisplay();
    }

    public void RemoveItems()
    {
        if (CurrentSlot == null) return;
        Inventory.instance.RemoveItems(CurrentSlot.Index);
        UpdateInventoryDisplay();
    }

    public void EquipItems()
    {
        if (CurrentSlot == null) return;
        Inventory.instance.EquipItems(CurrentSlot.Index);
        UpdateInventoryDisplay();
    }

    public void DrawItems(InventoryItems item, int index)
    {
        InventorySlot slot = slotList[index];
        Debug.Log($"Cập nhật ô {index}: Item = {item?.name ?? "null"}");
        if (item == null)
        {
            slot.ShowSlotInfo(false);
            return;
        }
        slot.UpdateSlot(item);
        slot.ShowSlotInfo(true);
    }

    public void ShowItemDescription(int index)
    {
        InventoryItems item = Inventory.instance.InventoryItems[index];
        if (item == null)
        {
            descriptionPanel.SetActive(false);
            return;
        }
        descriptionPanel.SetActive(true);
        itemsIcon.sprite = item.icon;
        itemsNameTMP.text = item.itemsName;
        itemsDescriptionTMP.text = item.description;
    }

    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (!inventoryPanel.activeSelf)
        {
            descriptionPanel.SetActive(false);
            CurrentSlot = null;
        }
    }

    private void SlotSlectedCallback(int slotIndex)
    {
        CurrentSlot = slotList[slotIndex];
        ShowItemDescription(slotIndex);
    }

    private void OnEnable()
    {
        InventorySlot.OnSlotSelectedEvent += SlotSlectedCallback;
    }

    private void OnDisable()
    {
        InventorySlot.OnSlotSelectedEvent -= SlotSlectedCallback;
    }
}