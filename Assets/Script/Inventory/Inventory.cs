using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Header")]

    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItems[] inventoryItems; 
    
    public int InventorySize => inventorySize;

    public void Start()
    {
        inventoryItems = new InventoryItems[inventorySize];
    }

    private void Update()
    {
        
    }

}
