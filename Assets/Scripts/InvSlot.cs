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
    [SerializeField] private string slotItemName = "";
    public string itemDialogText;
    private int list_index;

    [SerializeField] private Texture2D[] cursor;
    public string cursorSpriteName;
    public string useItemName;

    void Start()
    {
        //Cursor.SetCursor(cursor[0], Vector3.zero, CursorMode.ForceSoftware);
        inventoryManager = FindObjectOfType<InventoryManager_II>(); // get the Inventory script
        slotItemName = inventoryManager.pickedUpName; // get the name of the picked up item from Inventory

        for (int i = 0; i < itemScrObj.Length; i++) // make a name list of all the item names in the scriptable list
        {
            invenotryItemNames.Add(itemScrObj[i].itemName);
        }
        for (int i = 0; i < invenotryItemNames.Count; i++) // finding a match from the name list for the picked up item.
        {
            if (invenotryItemNames[i].Contains(slotItemName))
            {
                ItemScrObj pickedUpItem = itemScrObj[i]; // Take the correct scriptable object
                cursorSpriteName = itemScrObj[i].cursorSprite.name;
                list_index = i;
                gameObject.GetComponent<SpriteRenderer>().sprite = pickedUpItem.inventoryIcon; // Set correct item icon
                //cursorSpriteName = pickedUpItem.inventoryIcon.name;
                //Debug.Log("cursor nema: " + cursorSpriteName);
                //cursor = pickedUpItem.inventoryIcon;
                itemDialogText = pickedUpItem.itemText;
                if (inventoryManager.itemCount > 3)
                {
                    inventoryManager.Listener(itemDialogText); // Send item dialog to Inventory witch will set it on UI
                }
                
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1))//Right click will inspect the item
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                InvSlot invSlot = hit.transform.GetComponent<InvSlot>();
                if (invSlot != null)
                {
                    if (hit.transform.GetComponent<InvSlot>().slotItemName == this.slotItemName)
                    {
                        inventoryManager.Listener("It's a " + slotItemName);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0)) // Left click will use the item
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                InvSlot invSlot = hit.transform.GetComponent<InvSlot>();
                if (invSlot != null)
                {
                    if (hit.transform.GetComponent<InvSlot>().slotItemName == this.slotItemName)
                    {
                        for (int i = 0; i < cursor.Length; i++)
                        {
                            useItemName = cursorSpriteName;
                            if (cursor[i].name == cursorSpriteName)
                            {
                                Cursor.SetCursor(cursor[i], Vector3.zero, CursorMode.ForceSoftware);
                            }
                        }
                    }
                }
                else
                {
                    useItemName = "";
                    Cursor.SetCursor(null, Vector3.zero, CursorMode.ForceSoftware);
                }
            }
        }


    }

}