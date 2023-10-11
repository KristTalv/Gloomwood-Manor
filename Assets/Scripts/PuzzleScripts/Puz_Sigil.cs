using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_Sigil : MonoBehaviour
{
    public string statusSigil = "";
    //public string dialogLetLight;
    private InvSlot invSlot;
    //[SerializeField] public QuestDialogScrObj dialogSigil1;
    //[SerializeField] public QuestDialogScrObj dialogSigil2;
    public string diaPuzzSigil;
    //[SerializeField] private GameObject edwardsGrave;

    private InventoryManager_II inventoryManager;
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager_II>();
        statusSigil = "Violet";


    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            invSlot = FindObjectOfType<InvSlot>();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Puzzle")
                {
                    string cursorSprite = invSlot.useItemName;

                }
            }
        }


    }
}
