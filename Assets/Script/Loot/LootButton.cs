using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LootButton : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemsIcon;
    [SerializeField] private TextMeshProUGUI itemNames;
    [SerializeField] private TextMeshProUGUI itemQuantity;

    public DropItem itemLoad { get; private set; }

    public void ConfigLootButton(DropItem dropitem)
    {
        itemLoad = dropitem;
        itemsIcon.sprite = dropitem.Item.icon;
        itemNames.text = dropitem.Item.itemsName;
        itemQuantity.text = $"x{dropitem.Quantity.ToString()}";
    }   
    public void CollectItem()
    {
        if (itemLoad == null) return;
        Inventory.instance.AddItems(itemLoad.Item, itemLoad.Quantity);
        itemLoad.PickedItem = true;
        Destroy(gameObject);
    }    
   
}