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
    
    public InventorySlot CurrentSlot { get;  set; }  

    private List<InventorySlot> slotList = new List<InventorySlot>();

    protected override void Awake()
    {
        base.Awake();
        InitInventory();
    }

    private void Start()
    {
        
    }

    private void InitInventory()
    {
        for (int i = 0; i < Inventory.instance.InventorySize; i++)
        {
           InventorySlot slot = Instantiate(slotPrefab, container);
            slot.Index = i;
            slotList .Add(slot);
        }

    }

    public void UseItems()
    {
        if (CurrentSlot == null) return;
        Inventory.instance.UseItems(CurrentSlot.Index);
    }  
    
    public void RemoteItems()
    {
        if (CurrentSlot == null) return;
        Inventory.instance.RemoveItems(CurrentSlot.Index);

    }

    public void EquipItems() 
    {
        if (CurrentSlot == null) return;
        Inventory.instance.EquipItems(CurrentSlot.Index);
    }

    public void DrawItems(InventoryItems item, int index)
    {
        InventorySlot slot = slotList[index];
        if(item == null)
        {
            slot.ShowSlotInfo(false);
            return;
        }    
        
        slot.UpdateSlot(item);
        slot.ShowSlotInfo(true);
    }

    public void ShowItemDescription(int index)
    {
        if (Inventory.instance.InventoryItems[index] == null)
            return;
        descriptionPanel.SetActive(true);   
        itemsIcon.sprite = Inventory.instance.InventoryItems[index].icon;
        itemsNameTMP.text = Inventory.instance.InventoryItems[index].name;
        itemsDescriptionTMP.text = Inventory.instance.InventoryItems[index].description;

    }

    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf == false)
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
 