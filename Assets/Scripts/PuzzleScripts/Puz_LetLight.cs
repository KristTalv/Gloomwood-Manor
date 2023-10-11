using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puz_LetLight : MonoBehaviour
{
    public string statusLight = "";
    //public string dialogLetLight;
    private InvSlot invSlot;
    [SerializeField] public QuestDialogScrObj dialogLetLight1;
    [SerializeField] public QuestDialogScrObj dialogLetLight2;
    public string diaPuzzLetLight;

    private InventoryManager_II inventoryManager;
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager_II>();
        statusLight = "Violet";
        

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
                    if(cursorSprite == "Icon_Cursor_Lighter")
                    {
                        statusLight = "Green";
                        PuzzleManager puzzleMang = FindObjectOfType<PuzzleManager>();
                        puzzleMang.puz_LetLight_Status = statusLight;
                        diaPuzzLetLight= dialogLetLight2.dialogText;
                        inventoryManager.Listener(diaPuzzLetLight);
                        Destroy(gameObject);
                    }
                    else
                    {
                        diaPuzzLetLight = dialogLetLight1.dialogText;
                        inventoryManager.Listener(diaPuzzLetLight);
                    }
                }
            }
        }
        

    }
}
