using UnityEngine;

public class TestInventoryController : MonoBehaviour
{
    public TestInventory testInventory;  
    public TestInventoryUI testInventoryUI;  

    public Sprite testItemIcon1;  
    public Sprite testItemIcon2; 

    private void Start()
    {
        
        TestItem item1 = new TestItem("Healing Potion", testItemIcon1);
        TestItem item2 = new TestItem("Mana Potion", testItemIcon2);

       
        testInventory.AddTestItem(item1);
        testInventory.AddTestItem(item2);

        
        testInventoryUI.UpdateInventoryUI();
    }
}
