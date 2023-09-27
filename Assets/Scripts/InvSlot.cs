using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvSlot : MonoBehaviour
{
    [Header("All ScriptableObjects")]
    [SerializeField] private ItemScrObj[] itemScrObj; // All the item scribtaple objects
    [Header("Inventory Manager")]
    private InventoryManager_II inventoryManager;
    //[Header("Dialog Box")]
    [SerializeField] private GameObject dialogBox;

    List<string> invenotryItemNames = new List<string>(); // List of all the item names
    private string slotItemName = "";
    private string foundItemName = "";


    void Start()
    {
        // Hae scenestä dialogBox

        dialogBox = GameObject.Find("Text_DB");
        inventoryManager = FindObjectOfType<InventoryManager_II>(); // get the inventory script
        slotItemName = inventoryManager.pickedUpName; // get the name of the picked up item
        
        for (int i = 0; i < itemScrObj.Length; i++) // make a name list of all the item names in the scriptable list
        {
            invenotryItemNames.Add(itemScrObj[i].itemName);
        }
        for (int i = 0; i < invenotryItemNames.Count; i++) // finding a match from the name list for the picked up item.
        {
            if(invenotryItemNames[i].Contains(slotItemName))
            {
                ItemScrObj pickedUpItem = itemScrObj[i];
                gameObject.GetComponent<SpriteRenderer>().sprite = pickedUpItem.inventoryIcon;
                Debug.Log(gameObject.GetComponent<TextMeshProUGUI>().text);
                //dialogBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pickedUpItem.itemText;
                foundItemName = slotItemName;
                Debug.Log("Mätch " + foundItemName);

            }
        }

    }

    void Update()
    {
        
    }
}
