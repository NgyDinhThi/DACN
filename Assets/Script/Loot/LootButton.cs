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
        if (dropitem == null || dropitem.Item == null)
        {
            Debug.LogError($"dropitem or dropitem.Item is null! dropitem: {dropitem}, Item: {dropitem?.Item}");
            return;
        }
        if (dropitem.Item.Icon == null)
        {
            Debug.LogWarning($"Icon is null for item: {dropitem.Item.ItemsName}");
        }
        else
        {
            Debug.Log($"Setting icon for item: {dropitem.Item.ItemsName}, Icon: {dropitem.Item.Icon.name}");
        }
        itemsIcon.sprite = dropitem.Item.Icon;
        itemNames.text = dropitem.Item.ItemsName;
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