using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    // Item script handles: 
    // if player clicked item and enteret item area -->
    // pick up item -->
    // add item slot to inventory and destroy object from the scene

    // Strings
    string itemName = "";
    // Bools
    private bool isClicked = false;
    public bool isPickedUp = false;
    // Scriptable Objects
    public ItemScrObj itemObject;
    // Scripts
    private InventoryManager_II inventoryScript;
    private DialogManager dialogManager;
    private PuzzleManager puzzleManager;

    public void Start()
    {
        itemName = itemObject.itemName;
        isPickedUp = false;
        inventoryScript = FindObjectOfType<InventoryManager_II>();
        dialogManager = FindObjectOfType<DialogManager>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Chack if hitted object has itemscript --> is an item
                ItemScript itemScript = hit.transform.GetComponent<ItemScript>();
                if (itemScript != null)
                {
                    if (itemScript.itemName == itemName)
                    {
                        isClicked = true;
                        if(hit.transform.name == "Sir_Edward's_grave") // Sigil item managment
                        {
                            string status = puzzleManager.statusSigil;
                            if (status == "Yellow")
                            {
                                isClicked = true;
                            }
                        }
                    }
                }
                else
                {
                    isClicked = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player" && isClicked == true)
        {
            if (gameObject.transform.name == "Sir_Edward's_grave")
            {
                string status = puzzleManager.statusSigil;
                if (status == "Yellow")
                {
                    puzzleManager.statusSigil = "Green";
                    PickUpItem();
                }
                if (status == "Violet")
                {
                    dialogManager.Listener("Some dusty graves.");
                }
            }
            else
            {
                PickUpItem();
            }          
        }
    }

    private void PickUpItem()
    {
        GiveItemName(itemName);
        isPickedUp = true;
        inventoryScript.InventoryBool(isPickedUp);
        inventoryScript.AddToInventoryList(itemName);
        Destroy(gameObject);
    }

    public string GiveItemName(string pickUpName)
    {
        pickUpName = itemName;
        return pickUpName;
    }
}