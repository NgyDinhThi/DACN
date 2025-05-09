using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public static event Action<int> OnSlotSelectedEvent;


    [Header("Config")]
    [SerializeField] private Image itemsIcons;
    [SerializeField] private Image quantityContainer;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;
    
    public int Index { get;  set; }

    public void ClickSlot()
    {
        OnSlotSelectedEvent?.Invoke(Index);

    }    

    public void UpdateSlot(InventoryItems items)
    {
        itemsIcons.sprite = items.icon;
        itemQuantityTMP.text = items.quantity.ToString();
    }

    public void ShowSlotInfo(bool value)
    {
        itemsIcons.gameObject.SetActive(value);
        quantityContainer.gameObject.SetActive(value);

    }
}
