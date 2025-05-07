using UnityEngine;



[CreateAssetMenu(fileName = "ItemManaPotion", menuName = "Items/Mana potion")]

public class ItemManaPotion : InventoryItems
{
    [Header("Config")]

    public float ManaValue;

    public override bool UseItem()
    {
        if (GameManager.instance.Player.playerMana.CanRecoverMana())
        {
            GameManager.instance.Player.playerMana.RecoverMana(ManaValue);
            return true;
        }
        return false;
    }

}
