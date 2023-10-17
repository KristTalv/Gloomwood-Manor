using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InvSlot : MonoBehaviour
{
    // Integers
    private int list_index;
    // Strings
    public string cursorSpriteName;
    public string useItemName;
    [SerializeField] private string slotItemName = "";
    public string itemDialogText;
    // Lists
    List<string> invenotryItemNames = new List<string>(); // List of all the item names
    // Sprites
    [SerializeField] private Texture2D[] cursor;
    // Scriptable Objects
    [SerializeField] private ItemScrObj[] itemScrObj;
    // Scripts
    private InventoryManager_II inventoryManager;
    private DialogManager dialogManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager_II>(); 
        dialogManager = FindObjectOfType<DialogManager>(); 
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
                itemDialogText = pickedUpItem.itemText;
                if (inventoryManager.itemCount > 3)
                {
                    dialogManager.Listener(itemDialogText); // Send item dialog to Inventory witch will set it on UI
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
                        dialogManager.Listener("It's a " + slotItemName);
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