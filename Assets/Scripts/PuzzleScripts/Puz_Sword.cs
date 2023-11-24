using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_Sword : MonoBehaviour
{
    // Strings
    public string statusSword = "";
    private string statusSigil;
    private string cursorSprite;
    private string itemName;
    public string useItem = "Sigil";
    // Bools
    private bool isClicked = false;
    private bool isPickedUp = false;
    private bool isInRange = false;
    // Game Objects
    [SerializeField] private GameObject graveSwordObject;
    // ScribtableObjects
    [SerializeField] private ItemScrObj diaPuzzSword;
    // Scripts
    private DialogManager dialogManager;
    private PuzzleManager puzzleManager;
    private InventoryManager_II inventoryManagerII;
    private MouseController mouseController;

    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
        inventoryManagerII = FindObjectOfType<InventoryManager_II>();
        mouseController = FindObjectOfType<MouseController>();

        statusSword = "Violet";
        itemName = diaPuzzSword.itemName;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            statusSword = puzzleManager.statusSword;
            statusSigil = puzzleManager.statusSigil;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Q_Sword & KnightGrave")
                {
                    cursorSprite = mouseController.mouseScriptCursorName;
                    isClicked = true;
                    if(isInRange == true && puzzleManager.statusSigil == "Green" && cursorSprite == "Icon_Cursor_Symbol")
                    {
                        ExecuteSword();
                    }
                    else if (isInRange == true)
                    {
                        string message = diaPuzzSword.itemText;
                        dialogManager.Listener(message);
                    }
                }
                else if (hit.transform.name != "Q_Sword & KnightGrave")
                {
                    isClicked = false;
                }
            }
        }
    }
    private void ExecuteSword()
    {
        graveSwordObject.SetActive(false);
        statusSword = "Green";
        puzzleManager.statusSword = statusSword;
        string done_message = "The sword got off the statue! I'll take it.";
        dialogManager.Listener(done_message);
        PickUpItem();
    }
    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;

        if (isInRange == true && isClicked == true)
        {
            string message;

            if (statusSword == "Violet")
            {
                puzzleManager.statusSigil = "Yellow";
                puzzleManager.statusSword = "Yellow";

                message = diaPuzzSword.itemText;
                dialogManager.Listener(message);
            }

            if (statusSigil == "Green" && cursorSprite == "Icon_Cursor_Symbol")
            {
                ExecuteSword();
                inventoryManagerII.UseItem("Sigil");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isClicked = false;
        isInRange = false;
    }
    private void PickUpItem()
    {
        GiveSwordName(itemName);
        isPickedUp = true;
        inventoryManagerII.InventoryBool(isPickedUp);
        inventoryManagerII.AddToInventoryList(itemName);
    }
    
    public string GiveSwordName(string pickUpName)
    {
        pickUpName = itemName;
        return pickUpName;
    }
}
