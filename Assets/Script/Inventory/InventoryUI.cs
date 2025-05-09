using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryUI : Singleton<InventoryUI> 
{
    [Header("Config")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;
    
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
        Inventory.instance.UseItems(CurrentSlot.Index);
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

    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

    }

    private void SlotSlectedCallback(int slotIndex)
    {
        CurrentSlot = slotList[slotIndex];
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
 