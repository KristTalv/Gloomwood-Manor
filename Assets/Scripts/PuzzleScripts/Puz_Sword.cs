using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_Sword : MonoBehaviour
{
    // Strings
    public string statusSword = "";
    private string cursorSprite;
    private string itemName;
    public string useItem = "Sigil";
    // Bools
    private bool isClicked = false;
    private bool isPickedUp = false;
    // ScribtableObjects
    [SerializeField] private ItemScrObj diaPuzzSword;
    // Scripts
    private DialogManager dialogManager;
    private PuzzleManager puzzleManager;
    private InventoryManager_II inventoryManager_II;
    private MouseController mouseController;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
        inventoryManager_II = FindObjectOfType<InventoryManager_II>();
        mouseController = FindObjectOfType<MouseController>();
        statusSword = "Violet";
        itemName = diaPuzzSword.itemName;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Puzzle" && hit.transform.name == "Q_Sword & KnightGrave")
                {
                    cursorSprite = mouseController.mouseScriptCursorName;
                    isClicked = true;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(isClicked == true)
        {
            statusSword = puzzleManager.status_Sword;
            string statusSigil = puzzleManager.status_Sigil;
            if (statusSword == "Violet")
            {
                puzzleManager.status_Sigil = "Yellow";
                puzzleManager.status_Sword = "Yellow";
                string dialogOptio = diaPuzzSword.itemText;
                dialogManager.Listener(dialogOptio);
            }
            if(statusSigil == "Green")
            {
                if (cursorSprite == "Icon_Cursor_Symbol")
                {
                    statusSword = "Green";
                    puzzleManager.status_Sword = statusSword;
                    string message = "The sword got off the statue! I'll take it.";
                    dialogManager.Listener(message);
                    PickUpItem();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isClicked = false;
    }
    private void PickUpItem()
    {
        GiveSwordName(itemName);
        isPickedUp = true;
        inventoryManager_II.InventoryBool(isPickedUp);
        inventoryManager_II.AddToInventoryList(itemName);
        //Destroy(gameObject);
    }

    public string GiveSwordName(string pickUpName)
    {
        pickUpName = itemName;
        return pickUpName;
    }
}
