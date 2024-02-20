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
    private InventoryManager_II inventoryManagerII;


    void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
        inventoryManagerII = FindObjectOfType<InventoryManager_II>();

        statusSigil = "Violet";
        puzzleManager.statusSigil = statusSigil;
    }

    public void PickUpSigil()
    {
        itemName = "Sigil";
        inventoryManagerII.pickedUpName = itemName;
        GiveSigilName(itemName);
        isPickedUp = true;
        inventoryManagerII.InventoryBool(isPickedUp);
        inventoryManagerII.AddToInventoryList(itemName);
        puzzleManager.statusSigil = "Green";
    }

    public string GiveSigilName(string pickUpName)
    {
        pickUpName = itemName;
        return pickUpName;
    }
}
