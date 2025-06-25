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

    // Khởi tạo, xóa các slot cũ và load lại kho
    protected override void Awake()
    {
        base.Awake();
        ClearExistingSlots();
        InitInventory();
    }

    // Gọi cập nhật hiển thị khi game bắt đầu
    private void Start()
    {
        UpdateInventoryDisplay();
    }

    // Xóa các slot cũ đang hiển thị
    private void ClearExistingSlots()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        slotList.Clear();
    }

    // Tạo mới các slot tương ứng với kích thước kho
    private void InitInventory()
    {
        for (int i = 0; i < Inventory.instance.InventorySize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.Index = i;
            slotList.Add(slot);
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
    }

    // Cập nhật lại toàn bộ giao diện kho
    public void UpdateInventoryDisplay()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            InventoryItems item = Inventory.instance.InventoryItems[i];
            DrawItems(item, i);
        }
    }

    // Sử dụng vật phẩm tại slot đang chọn
    public void UseItems()
    {
        if (CurrentSlot == null) return;
        Inventory.instance.UseItems(CurrentSlot.Index);
        UpdateInventoryDisplay();
    }

    // Xóa vật phẩm tại slot đang chọn
    public void RemoveItems()
    {
        if (CurrentSlot == null) return;
        Inventory.instance.RemoveItems(CurrentSlot.Index);
        UpdateInventoryDisplay();
    }

    // Trang bị vật phẩm tại slot đang chọn
    public void EquipItems()
    {
        if (CurrentSlot == null) return;
        Inventory.instance.EquipItems(CurrentSlot.Index);
        UpdateInventoryDisplay();
    }

    // Hiển thị vật phẩm lên slot UI tương ứng
    public void DrawItems(InventoryItems item, int index)
    {
        InventorySlot slot = slotList[index];
        if (item == null)
        {
            slot.ShowSlotInfo(false);
            return;
        }
        slot.UpdateSlot(item);
        slot.ShowSlotInfo(true);
    }

    // Hiển thị thông tin vật phẩm tại slot được chọn
    public void ShowItemDescription(int index)
    {
        InventoryItems item = Inventory.instance.InventoryItems[index];
        if (item == null)
        {
            descriptionPanel.SetActive(false);
            return;
        }
        descriptionPanel.SetActive(true);
        itemsIcon.sprite = item.Icon;
        itemsNameTMP.text = item.ItemsName;
        itemsDescriptionTMP.text = item.description;
    }

    // Bật/tắt giao diện kho
    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (!inventoryPanel.activeSelf)
        {
            descriptionPanel.SetActive(false);
            CurrentSlot = null;
        }
    }

    // Xử lý khi một slot được chọn
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
