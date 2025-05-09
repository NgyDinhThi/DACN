using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI> 
{
    [Header("Config")]
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;
    
    private List<InventorySlot> slotList = new List<InventorySlot>();

    private void Start()
    {
        InitInventory();
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
    
    public void DrawItems(InventoryItems items, int index)
    {
        InventorySlot slot = slotList[index];
        slot.ShowSlotInfo(true);
        slot.UpdateSlot(items);
          
    }    


}
 