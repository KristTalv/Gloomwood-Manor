using System.Collections.Generic;
using UnityEngine;

public class InvSlot : MonoBehaviour
{
    // Strings
    public string cursorSpriteName;
    public string useItemName;
    [SerializeField] private string slotItemName = "";
    public string itemDialogText;
    // Lists
    List<string> invenotryItemNameList = new List<string>(); // List of all the item names
    // Sprites
    [SerializeField] private Texture2D[] cursorTexture;
    // Scriptable Objects
    [SerializeField] private ItemScrObj[] itemScrObj;
    [SerializeField] private QuestDialogScrObj sigilDialogInventory;
    [SerializeField] private QuestDialogScrObj swordDialogInventory;
    // Scripts
    private InventoryManager_II inventoryManager;
    private DialogManager dialogManager;
    private MouseController mouseController;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager_II>();
        dialogManager = FindObjectOfType<DialogManager>();
        mouseController = FindObjectOfType<MouseController>();
        slotItemName = inventoryManager.pickedUpName; // get the name of the picked up item from Inventory

        for (int i = 0; i < itemScrObj.Length; i++) // make a name list of all the item names in the scriptable list
        {
            invenotryItemNameList.Add(itemScrObj[i].itemName);
        }
        for (int i = 0; i < invenotryItemNameList.Count; i++) // finding a match from the name list for the picked up item.
        {
            if (invenotryItemNameList[i].Contains(slotItemName))
            {
                ItemScrObj pickedUpItem = itemScrObj[i]; // Take the correct scriptable object
                cursorSpriteName = itemScrObj[i].cursorSprite.name;
                gameObject.GetComponent<SpriteRenderer>().sprite = pickedUpItem.inventoryIcon; // Set correct item icon
                itemDialogText = pickedUpItem.itemText;
                if (inventoryManager.itemCount > 3 && pickedUpItem.itemName != "Sword")
                {
                    dialogManager.Listener(itemDialogText); // Send item dialog to Inventory witch will set it on UI
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))//Right click will inspect the item
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
                        Debug.Log(this.slotItemName);
                        string message = "";
                        if (this.slotItemName == "Sigil")
                        {
                            message = sigilDialogInventory.dialogText;
                        }
                        else if (this.slotItemName == "Sword")
                        {
                            message = swordDialogInventory.dialogText;
                        }
                        else
                        {                          
                            message = this.itemDialogText;
                        }
                        dialogManager.Listener(message);
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
                        for (int i = 0; i < cursorTexture.Length; i++)
                        {
                            useItemName = cursorSpriteName;
                            if (cursorTexture[i].name == cursorSpriteName)
                            {
                                mouseController.SetCursor(cursorSpriteName);
                            }
                        }
                    }
                }
                else
                {
                    useItemName = "";
                    mouseController.NulCursor(useItemName);
                }
            }
        }
    }
}