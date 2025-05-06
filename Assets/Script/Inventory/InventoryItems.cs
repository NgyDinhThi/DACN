using UnityEngine;


public enum Itemtype
{
    Weapon,
    Potion,
    Scroll,
    Ingredients,
    Treasure
}

[CreateAssetMenu(menuName = "Items/Item")]

public class InventoryItems : ScriptableObject
{
    [Header("Config")]

    public string id;
    public string name;
    public Sprite icon;
    [TextArea]public string mieuta;

    [Header("Info")]
    public Itemtype itemtype;
    public bool IsComsumable;
    public bool IsStackable;
    public int MaxStack;


    [HideInInspector]public int Quantity;
    
    public InventoryItems CopyItem()
    {
     InventoryItems instance = Instantiate(this);
        return instance;
    
    }

    public virtual bool UseItem()
    {
        return true;

    }

    public virtual void EquipItem()
    {

    }

    public virtual void RemoveItem()
    {



    }

}
