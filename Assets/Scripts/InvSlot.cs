using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvSlot : MonoBehaviour
{
    [Header("All ScriptableObjects")]
    [SerializeField] private ItemScrObj[] itemScrObj; // All the item scribtaple objects
    [Header("Inventory Manager")]
    private InventoryManager_II inventoryManager;

    List<string> invenotryItemNames = new List<string>(); // List of all the item names
    private string slotItemName = "";


    void Start()
    {
        
        inventoryManager = FindObjectOfType<InventoryManager_II>();
        slotItemName = inventoryManager.pickedUpName;
        
        for (int i = 0; i < itemScrObj.Length; i++)
        {
            invenotryItemNames.Add(itemScrObj[i].itemName);
        }
        for (int i = 0; i < invenotryItemNames.Count; i++)
        {
            if(invenotryItemNames[i].Contains(slotItemName))
            {
                Debug.Log("Mätch " + slotItemName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
