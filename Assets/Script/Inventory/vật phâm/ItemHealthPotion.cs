using UnityEngine;


[CreateAssetMenu(fileName = "ItemHealthPotion", menuName = "Items/Health potion")]

public class ItemHealthPotion : InventoryItems
{
    [Header("Config")]
    public float Healthvalue;

    public override bool UseItem()
    {
       if (GameManager.instance.Player.playerhealth.CanRestoreHealth())
        {
            GameManager.instance.Player.playerhealth.RestoredHealth(Healthvalue);
            return true;
        }    
       return false;
    }


}