using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour
{
    [Header("All ScriptableObjects")]
    [SerializeField] private ItemScrObj[] itemScrObj; // All the item scribtaple objects
    [Header("Inventory Manager")]
    private InventoryManager_II inventoryManager; // Inventory script

    List<string> invenotryItemNames = new List<string>(); // List of all the item names
    private string slotItemName = "";
    public string itemDialogText; 

    void Start()
    {

        inventoryManager = FindObjectOfType<InventoryManager_II>(); // get the Inventory script
        slotItemName = inventoryManager.pickedUpName; // get the name of the picked up item from Inventory
        
        for (int i = 0; i < itemScrObj.Length; i++) // make a name list of all the item names in the scriptable list
        {
            invenotryItemNames.Add(itemScrObj[i].itemName);
        }
        for (int i = 0; i < invenotryItemNames.Count; i++) // finding a match from the name list for the picked up item.
        {
            if(invenotryItemNames[i].Contains(slotItemName))
            {
                ItemScrObj pickedUpItem = itemScrObj[i]; // Take the correct scriptable object
                gameObject.GetComponent<SpriteRenderer>().sprite = pickedUpItem.inventoryIcon; // Set correct item icon
                itemDialogText = pickedUpItem.itemText;
                inventoryManager.Listener(itemDialogText); // Send item dialog to Inventory witch will set it on UI
            }
        }
    }

    void Update()
    {


    }
}
