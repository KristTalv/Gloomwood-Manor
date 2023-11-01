using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_Sigil : MonoBehaviour
{
    // Strings
    public string statusSigil = "";
    public string diaPuzzSigil;
    private string itemName;
    // Bools
    private bool isPickedUp = false;
    // Scriptable Objects
    [SerializeField] private ItemScrObj itemScrObj;
    // Scripts
    private PuzzleManager puzzleManager;
    private InventoryManager_II inventoryManager_II;


    void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
        inventoryManager_II = FindObjectOfType<InventoryManager_II>();

        statusSigil = "Violet";
        puzzleManager.status_Sigil = statusSigil;
    }

    public void PickUpSigil()
    {
        itemName = "Sigil";
        inventoryManager_II.pickedUpName = itemName;
        GiveSigilName(itemName);
        isPickedUp = true;
        inventoryManager_II.InventoryBool(isPickedUp);
        inventoryManager_II.AddToInventoryList(itemName);
    }

    public string GiveSigilName(string pickUpName)
    {
        pickUpName = itemName;
        return pickUpName;
    }
}
